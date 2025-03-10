using ML;
using System;
using System.Collections.Generic;
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
            HttpPostedFileBase file = Request.Files["inptFileImgUsuario"];
            if (file != null)
            {
                usuario.Imagen = ConvertirArrayBytes(file);
            }

            if (usuario.IdUsuario == 0)
            {
                //Si no encuentra un registro con ese ID, crea uno nuevo 
                BL.Usuario.AddEF(usuario); //Ejecuta el metodo de agregar de EF
            }
            else
            {
                //Si el registro existe, actualiza los datos
                ML.Result result = BL.Usuario.UpdateEF(usuario); //Ejecuta el metodo de actualizar de EF 
            }
            return RedirectToAction("GetAll"); //Redirige a la pagina principal
        }


        // DELETE
        [HttpGet]
        public ActionResult Delete(int? idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.DeleteEF(idUsuario.Value);

            return RedirectToAction("GetAll");
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

    }
}