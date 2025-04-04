using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebAPI.Controllers
{
    [RoutePrefix("api")]
    public class CrudAPIController : ApiController
    {
        [HttpGet]
        [Route("Crud/GetAll")]
        public IHttpActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();

            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;

            ML.Result result = BL.Usuario.GetAllEF(usuario);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            } else
            {
                return Content(HttpStatusCode.InternalServerError, result);
            }
        }

        [HttpGet]
        [Route("Crud/GetById/{idUsuario}")]
        public IHttpActionResult GetById(int idUsuario)
        {
            ML.Result result = BL.Usuario.GetByIdEF(idUsuario);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            } else
            {
                return Content(HttpStatusCode.InternalServerError, result);
            }

        }

        [HttpPost]
        [Route("Crud/Add")]
        public IHttpActionResult Add([FromBody] ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.AddEF(usuario);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, result);
            }
        }

        [HttpPut]
        [Route("Crud/Update/{idUsuario}")]
        public IHttpActionResult Update(int idUsuario, [FromBody]ML.Usuario usuario)
        {
            usuario.IdUsuario = idUsuario;
            ML.Result result = BL.Usuario.UpdateEF(usuario);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, result);
            }
        }

        [HttpDelete]
        [Route("Crud/Delete/{idUsuario}")]
        public IHttpActionResult Delete(int idUsuario)
        {
            ML.Result result = BL.Usuario.DeleteEF(idUsuario);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, result);
            }
        }

        
    }
}
