using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace PL_MVC.Controllers
{
    public class CrudJSController : Controller
    {
        // GET: CrudJS
        public ActionResult CrudJS()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;

            ML.Result result = BL.Usuario.GetAllEF(usuario);

            JsonResult jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        [HttpGet]
        public JsonResult Delete(int? idUsuario)
        {
            ML.Result JsonResult = BL.Usuario.DeleteEF(idUsuario.Value);

            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetById(int idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

            ML.Result JsonResult = BL.Usuario.GetByIdEF(idUsuario);

            if (JsonResult.Correct)
            {
                ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

                ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;

                ML.Result resultRol = BL.Rol.GetAll();
                usuario.Rol.Roles = resultRol.Objects;

                ML.Result resultEstado = BL.Estado.GetAll();
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;

            }

            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }

        public byte[] ConvertirArrayBytes(HttpPostedFileBase Foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);

            return data;
        }


        [HttpPost]
        public JsonResult Form(ML.Usuario usuario)
        {
            ML.Result JsonResult = new ML.Result();

            if (!ModelState.IsValid)
            {
                // Volver a llenar listas desplegables en caso de error
                ML.Result resultRol = BL.Rol.GetAll();
                usuario.Rol.Roles = resultRol.Objects;

                ML.Result resultEstado = BL.Estado.GetAll();
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;

                ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;

                ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

            }

            // Manejo de imagen
            HttpPostedFileBase file = Request.Files["Imagen"];
            if (file != null)
            {
                usuario.Imagen = ConvertirArrayBytes(file);
            }

            if (usuario.IdUsuario == 0)
            {
                // AGREGAR NUEVO USUARIO
                JsonResult = BL.Usuario.AddEF(usuario);
            }
            else
            {
                // ACTUALIZAR USUARIO EXISTENTE
                JsonResult = BL.Usuario.UpdateEF(usuario);
            }

            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }






        //Extrae roles
        [HttpGet]
        public JsonResult DDLRol()
        {
            ML.Result resultRol = BL.Rol.GetAll();
            return Json(resultRol, JsonRequestBehavior.AllowGet);
        }

        //Extrae estados
        [HttpGet]
        public JsonResult DDLEstado()
        {
            ML.Result resultEstado = BL.Estado.GetAll();
            return Json(resultEstado, JsonRequestBehavior.AllowGet);
        }

        //Extrae municipios
        [HttpGet]
        public JsonResult DDLMunicipio(int idEstado)
        {
            ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(idEstado);
            return Json(resultMunicipio, JsonRequestBehavior.AllowGet);
        }

        //Extrae colonias
        [HttpGet]
        public JsonResult DDLColonia(int idMunicipio)
        {
            ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(idMunicipio);
            return Json(resultColonia, JsonRequestBehavior.AllowGet);
        }




    }
}