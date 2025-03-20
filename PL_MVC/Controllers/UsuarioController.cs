using Microsoft.Owin.Security.Provider;
using ML;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {

        // GET ALL
        [HttpGet] //Decorador. Indica que la pagina web va a recibir
        public ActionResult GetAll() //El nombre del metodo es GetAll y va a devolver una vista
        {
            ML.Usuario usuario = new ML.Usuario(); //Se crea un modelo nuevo de usuario
            usuario.Rol = new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;
            ML.Result result = BL.Usuario.GetAllEF(usuario); //Se agrega al modelo result el return del GetAllEF

            if (result.Correct)
            {
                //Si obtuvo toda la informacion usa la propiedad objects del result
                usuario.Usuarios = result.Objects; //Al modelo usuario en su propiedad usuarios (lista de objetos) se agrega la lista del objetos del result 
            }
            else
            {
                //No se obtuvieron registros
                //Imprime un mensaje de error 
                usuario.Usuarios = new List<object>(); //Se crea una nueva lista vacia de Usuarios 
            }

            ML.Result resultDDLRol = BL.Rol.GetAll();
            usuario.Rol.Roles = resultDDLRol.Objects;

            return View(usuario); //Devuelve las vista. La vista solo puede retornar un solo tipo de dato. En este caso devuelve 
        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            //Ternarios
            usuario.Nombre = usuario.Nombre == null ? "" : usuario.Nombre; //A usuario.Nombre le asigna el resultado del ternario. Si usuario.Nombre es nulo, se asigna un string vacío, si no es nulo se asigna el valor que ya trae. El ? indica cuando se acaba la condición. Los ternarios cumplen la misma función que un if pero disminuyen código
            usuario.ApellidoPaterno = usuario.ApellidoPaterno == null ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = usuario.ApellidoMaterno == null ? "" : usuario.ApellidoMaterno;
            usuario.Rol.IdRol = usuario.Rol.IdRol == 0 ? 0 : usuario.Rol.IdRol; //Si el valor de idRol es 0, se mantiene el 0, sino se mantiene el valor que ya trae

            ML.Result result = BL.Usuario.GetAllEF(usuario);
            usuario.Usuarios = result.Objects;

            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Roles = resultDDL.Objects;

            return View(usuario);
        }

        // AGREGAR / ACTUALIZAR
        [HttpGet] //Decorador de metodo Form para obtener informacion
        public ActionResult Form(int? IdUsuario) //El IdUsuario puede ser nulo o no
        {
            ML.Usuario usuario = new ML.Usuario(); //Nuevo modelo de usuario

            if (IdUsuario == null) //Se evalua si es nulo o no para saber que tipo de formulario se necesita (agregar o actualizar)
            {
                //ADD
                //Se manda un formulario vacio 
                usuario.Rol = new ML.Rol(); //Se abre la puerta para poder entar al modelo desde el modelo usuario

                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            }
            else //El IdUsuario no es nulo
            {
                //ACTUALIZAR
                //Se manda el formulario lleno con la informacion del id solicitado
                ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value); //Al modelo result en la propiedad object se le asigna el valor del GBI con el parametro de IdUsuario porque solo va a tener un registro
                usuario = (ML.Usuario)result.Object; //UNBOXING. Al modelo usuario se le asigna el usuario que se va a sacar de la caja del result en la propiedad object.
                ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

                ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
            }

            ML.Result resultRol = BL.Rol.GetAll(); //Al metodo result de roles se le asigna el result del GetAll de roles
            usuario.Rol.Roles = resultRol.Objects; //Entramos al modelo usuario, despues al modelo rol para agarrar su propiedad de lista de roles en donde se guardaron los objetos rol despues de haber ejecutado el GetAll
                                                   //usuario.Rol = new ML.Rol(); //Si abrimos la puerta de nuevo, el modelo se reescribe y perdemos la informacion

            ML.Result resultEstado = BL.Estado.GetAll();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;

            return View(usuario);
        }


        // ACTUALIZAR / AGREGAR
        [HttpPost] //Decorador de metodo Form para agregar informacion
        public ActionResult Form(ML.Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                //regresa informacion dio
                //llenar ddl
                ML.Result resultRol = BL.Rol.GetAll();
                usuario.Rol.Roles = resultRol.Objects;

                ML.Result resultEstado = BL.Estado.GetAll();
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;

                ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;

                ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

                return View(usuario);
            }

            HttpPostedFileBase file = Request.Files["inptFileImgUsuario"];
            if (file != null)
            {
                usuario.Imagen = ConvertirArrayBytes(file);
            }

            if (usuario.IdUsuario == 0)
            {
                //Si no encuentra un registro con ese ID, crea uno nuevo 
                BL.Usuario.AddEF(usuario); //Ejecuta el metodo de agregar de EF
                ViewBag.MensajeBLUsuario = "Se realizó la inserción correctamente";
            }
            else
            {
                //Si el registro existe, actualiza los datos
                ML.Result result = BL.Usuario.UpdateEF(usuario); //Ejecuta el metodo de actualizar de EF 
                ViewBag.MensajeBLUsuario = "Se realizó la actualización correctamente";
            }
            return PartialView("_ResultBLUsuario"); //Redirige a la pagina principal
        }

        // DELETE
        [HttpGet]
        public ActionResult Delete(int? idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.DeleteEF(idUsuario.Value);
            ViewBag.MensajeBLUsuario = "Se eliminó el registro correctamente";


            return PartialView("_ResultBLUsuario");
        }

        //CAMBIAR ESTATUS
        [HttpPost]
        public JsonResult CambiarEstatus(int idUsuario, bool estatus)
        {
            ML.Result JsonResult = BL.Usuario.CambiarEstatus(idUsuario, estatus); //El modelo result se llama JsonResult y recibe el result del metodo CambiarEstatus del BL
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        //DDL ESTADO
        [HttpGet]
        public JsonResult MunicipioGetByIdEstado(int idEstado)
        {
            ML.Result JsonResult = BL.Municipio.GetByIdEstado(idEstado);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ColoniaGetByIdMunicipio(int idMunicipio)
        {
            ML.Result JsonResult = BL.Colonia.GetByIdMunicipio(idMunicipio);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        public byte[] ConvertirArrayBytes(HttpPostedFileBase Foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);

            return data;
        }

        public ActionResult CargaMasivaExcel()
        {
            if (Session["RutaExcel"] == null)
            {

                HttpPostedFileBase excelUsuario = Request.Files["ExcelUsuario"]; //Cachamos el excel del usuario

                string extensionValida = ".xlsx"; //Declaramos la extension permitida

                if (excelUsuario.ContentLength > 0) //Si el contenido del usuario es mayor a 0
                {
                    //El usuario si adjuntó un archivo
                    string extensionArchivoUsuario = Path.GetExtension(excelUsuario.FileName); //Obtenemos la extension del archivo que nos adjunto el usuario

                    if (extensionArchivoUsuario == extensionValida) //Si la extension del archivo adjunto es valida
                    {
                        string ruta = Server.MapPath("~/CargaMasiva/") + Path.GetFileNameWithoutExtension(excelUsuario.FileName) + "-" + DateTime.Now.ToString("ddMMyyyyHHmmssfff") + ".xlsx"; //Se crea una copia del archivo excel. Se le asigna la ruta relativa de donde se va a guardar, se obtiene el nombre del archivo sin extension, se le concatena un - y la fecha y hora para hacerlo unico, despues se le adjunta la extension .xlsx

                        if (!System.IO.File.Exists(ruta)) //Si el archivo no existe en la ruta especificada se guarda y se crea uan cadena de conexion para el archivo con el proveedor ole db
                        {
                            excelUsuario.SaveAs(ruta); //Se guarda el archivo en en la ruta y con el nombre especificado

                            string cadenaConexion = ConfigurationManager.ConnectionStrings["OleDbConnection"] + ruta; //Creacion de la cadena de conexion
                            ML.Result resultExcel = BL.Usuario.LeerExcel(cadenaConexion);

                            if (resultExcel.Objects.Count > 0)
                            {
                                ML.ResultExcel resultValidacion = BL.Usuario.ValidarExcel(resultExcel.Objects);

                                if (resultValidacion.Errores.Count > 0)
                                {
                                    ViewBag.ErroresExcel = resultValidacion.Errores;
                                    return PartialView("_ErroresExcel");
                                }
                                else
                                {
                                    Session["RutaExcel"] = ruta;
                                }
                            }
                        }
                        else
                        {
                            //Vuelve a cargar el archivo porque ya existe
                            ViewBag.MensajeError = "El archivo ya existe, vuelve a cargar uno";
                            ViewBag.ErroresExcel = new List<object>();
                            return PartialView("_ErroresExcel");
                        }
                    }
                    else
                    {
                        //El archivo no es un excel
                        ViewBag.MensajeError = "El archivo adjunto no es un Excel";
                        ViewBag.ErroresExcel = new List<object>();
                        return PartialView("_ErroresExcel");
                    }
                }
                else
                {
                    //El usuario no adjuntó un archivo
                    ViewBag.MensajeError = "No adjuntaste un archivo";
                    ViewBag.ErroresExcel = new List<object>();
                    return PartialView("_ErroresExcel");
                }

            }
            else
            {
                string cadenaConexion = ConfigurationManager.ConnectionStrings["OleDbConnection"] + Session["RutaExcel"].ToString();

                ML.Result resultLeer = BL.Usuario.LeerExcel(cadenaConexion);

                if (resultLeer.Objects.Count > 0)
                {
                    int contador = 1;

                    foreach (ML.Usuario usuario in resultLeer.Objects)
                    {
                        //contador++;
                        //if (ModelState.IsValid)
                        //{
                            ML.Result resultInsertar = BL.Usuario.AddEF(usuario);
                            if (!resultInsertar.Correct)
                            {
                                //Mostrar el error que salió
                                ViewBag.MensajeError = "Hubo un error en la inserción del usuario " + contador;
                                ViewBag.ErroresExcel = new[] { resultInsertar };
                            }
                        //}
                        //else
                        //{
                        //    ViewBag.ErroresExcel = "El usuario " + contador + " tiene uno o más campos incorrectos";
                        //}
                    }
                    ViewBag.MensajeCorrecto = "La inserción se hizo correctamente";
                    ViewBag.ErroresExcel = new List<object>();
                    return PartialView("_ErroresExcel");
                }
                Session["RutaExcel"] = null;
            }
            return RedirectToAction("GetAll");
        }
    }
}