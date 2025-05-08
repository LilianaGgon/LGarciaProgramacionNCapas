using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class CitaController : Controller
    {
        // GET: Cita
        [HttpGet]
        public ActionResult Cita()
        {
            ML.Cita cita = new ML.Cita();
            cita.Candidato = new ML.Candidato();
            cita.Candidato.Vacante = new ML.Vacante();

            ML.Result resultVacante = BL.Vacante.GetAll();
            cita.Candidato.Vacante.Vacantes = resultVacante.Objects;

            cita.Citas = new List<object>();

            return View(cita);
        }

        [HttpPost]
        public ActionResult Cita(int? idVacante)
        {
            idVacante = idVacante == null ? 0 : idVacante;

            ML.Cita cita = new ML.Cita();
            cita.Candidato = new ML.Candidato();
            cita.Candidato.Vacante = new ML.Vacante();

            if (idVacante != 0)
            {
                ML.Result result = BL.Cita.GetAll(idVacante.Value);
                if (result.Objects == null)
                {
                    cita.Citas = new List<object>();
                }
                else
                {
                    cita.Citas = result.Objects;
                }
            }
            else
            {
                Cita();
            }

            ML.Result resultVacante = BL.Vacante.GetAll();
            cita.Candidato.Vacante.Vacantes = resultVacante.Objects;

            return View(cita);
        }

        [HttpGet]
        public ActionResult FormCita(int? idCandidato)
        {
            ML.Cita cita = new ML.Cita();

            if (idCandidato == null)
            {
                cita.EstatusCita = new ML.EstatusCita();
                cita.Piso = new ML.Piso();
                cita.Candidato = new ML.Candidato();
                cita.Candidato.Vacante = new ML.Vacante();
            }
            else
            {
                ML.Result resultCita = BL.Cita.GetById(idCandidato.Value);
                cita = (ML.Cita)resultCita.Object;
            }

            ML.Result resultPiso = BL.Piso.GetAll();
            cita.Piso.Pisos = resultPiso.Objects;

            ML.Result resultEstatusCita = BL.EstatusCita.GetAll();
            cita.EstatusCita.EstatusCitas = resultEstatusCita.Objects;

            return View(cita);
        }

        [HttpPost]
        public ActionResult FormCita(ML.Cita cita)
        {
            //if (cita.Piso.IdPiso == 0)
            //{
            //    cita.Piso.IdPiso = Convert.ToByte(null);
            //}

            if (cita.IdCita == 0)
            {
                cita.EstatusCita = new ML.EstatusCita();
                cita.EstatusCita.IdEstatusCita = 1;
                ML.Result result = BL.Cita.Add(cita);
            }
            else
            {
                ML.Result result = BL.Cita.Update(cita);
            }

            return RedirectToAction("Cita");
        }

        [HttpGet]
        public ActionResult Delete(int? idCita)
        {
            ML.Result result = BL.Cita.Delete(idCita.Value);

            return RedirectToAction("Cita");
        }

    }
}