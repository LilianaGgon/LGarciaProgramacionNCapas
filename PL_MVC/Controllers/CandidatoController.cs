using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Activities;

namespace PL_MVC.Controllers
{
    public class CandidatoController : Controller
    {
        // GET: Candidato
        [HttpGet]
        public ActionResult Candidato()
        {
            ML.Candidato candidato = new ML.Candidato();
            candidato.Vacante = new ML.Vacante();

            ML.Result resultVacante = BL.Vacante.GetAll();
            candidato.Vacante.Vacantes = resultVacante.Objects;
            candidato.Candidatos = new List<object>();

            return View(candidato);
        }

        [HttpPost]
        public ActionResult Candidato(int? IdVacante)
        {
            IdVacante = IdVacante == null ? 0 : IdVacante;

            ML.Candidato candidato = new ML.Candidato();
            candidato.Vacante = new ML.Vacante();

            if (IdVacante != 0)
            {

                ML.Result result = BL.Candidato.GetAllBusqueda(IdVacante.Value);

                if (result.Objects == null)
                {
                    candidato.Candidatos = new List<object>();
                }
                else
                {
                    candidato.Candidatos = result.Objects;
                }
            }
            else
            {
                candidato.Candidatos = new List<object>();
                Candidato();
            }

            ML.Result resultVacante = BL.Vacante.GetAll();
            candidato.Vacante.Vacantes = resultVacante.Objects;

            return View(candidato);
        }

        [HttpGet]
        public ActionResult FormCandidato(int? idCandidato)
        {
            ML.Candidato candidato = new ML.Candidato();

            if (idCandidato == null)
            {
                candidato.Universidad = new ML.Universidad();
                candidato.BolsaTrabajo = new ML.BolsaTrabajo();
                candidato.Vacante = new ML.Vacante();
                candidato.Carrera = new ML.Carrera();
            }
            else
            {
                ML.Result result = BL.Candidato.GetById(idCandidato.Value);
                candidato = (ML.Candidato)result.Object;
            }


            ML.Result resultUniversidad = BL.Universidad.GetAll();
            candidato.Universidad.Universidades = resultUniversidad.Objects;

            ML.Result resultCarrera = BL.Carrera.GetAll();
            candidato.Carrera.Carreras = resultCarrera.Objects;

            ML.Result resultVacante = BL.Vacante.GetAll();
            candidato.Vacante.Vacantes = resultVacante.Objects;

            ML.Result resultBolsaTrabajo = BL.BolsaTrabajo.GetAll();
            candidato.BolsaTrabajo.BolsasTrabajo = resultBolsaTrabajo.Objects;

            return View(candidato);
        }

        [HttpPost]
        public ActionResult FormCandidato(ML.Candidato candidato)
        {
            if (!ModelState.IsValid)
            {
                ML.Result resultUniversidad = BL.Universidad.GetAll();
                candidato.Universidad.Universidades = resultUniversidad.Objects;

                ML.Result resultCarrera = BL.Carrera.GetAll();
                candidato.Carrera.Carreras = resultCarrera.Objects;

                ML.Result resultVacante = BL.Vacante.GetAll();
                candidato.Vacante.Vacantes = resultVacante.Objects;

                ML.Result resultBolsaTrabajo = BL.BolsaTrabajo.GetAll();
                candidato.BolsaTrabajo.BolsasTrabajo = resultBolsaTrabajo.Objects;

                return View(candidato);
            }

            HttpPostedFileBase foto = Request.Files["inptFoto"];
            if (foto.ContentLength <= 0)
            {
                candidato.Foto = candidato.Foto;
            }
            else
            {
                candidato.Foto = ConvertirArrayBytes(foto);
            }

            HttpPostedFileBase curriculum = Request.Files["inptCurriculum"];
            if (curriculum.ContentLength <= 0)
            {
                candidato.Curriculum = candidato.Curriculum;
            }
            else
            {
                candidato.Curriculum = ConvertirArrayBytes(curriculum);
            }


            if (candidato.IdCandidato == 0)
            {
                ML.Result result = BL.Candidato.Add(candidato);
            }
            else
            {
                ML.Result result = BL.Candidato.Update(candidato);
            }

            return RedirectToAction("Candidato");
        }

        [HttpGet]
        public ActionResult Delete(int? idCandidato)
        {
            ML.Result result = BL.Candidato.Delete(idCandidato.Value);

            return RedirectToAction("Candidato");
        }


        public byte[] ConvertirArrayBytes(HttpPostedFileBase Archivo)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Archivo.InputStream);
            byte[] data = reader.ReadBytes((int)Archivo.ContentLength);

            return data;
        }


        [HttpGet]
        public FileResult DescargarCV(int idCantidato)
        {
            ML.Result result = BL.Candidato.GetById(idCantidato);
            ML.Candidato candidato = (ML.Candidato)result.Object;
            byte[] archivo = candidato.Curriculum;
            string nombreCV = "CV_" + candidato.Nombre + "_" + candidato.ApellidoPaterno + "_" + candidato.ApellidoMaterno + ".pdf";

            if (archivo != null && archivo.Length > 0)
            {
                return File(archivo, System.Net.Mime.MediaTypeNames.Application.Octet, nombreCV);
            }
            else
            {
                return null;
            }

        }

        //public FileResult Descargar(int idCantidato)
        //{

        //    byte[] fileBytes = System.IO.File.ReadAllBytes(@"\\svr\dllo\Pendientes\" + nombrePDF);
        //    return File(fileBytes, "application/pdf");
        //}



    }
}