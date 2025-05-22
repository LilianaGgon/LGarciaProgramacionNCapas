using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
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
                cita.Citas = new List<object>();
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

                if (result.Correct)
                {
                    EnviarCorreo(cita);
                }
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


        [NonAction]
        public ActionResult EnviarCorreo(ML.Cita cita)
        {
            try
            {
                string correo = ConfigurationManager.AppSettings["Correo"].ToString();
                string password = ConfigurationManager.AppSettings["Contraseña"].ToString();
                int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"]);

                string body = "";
                string path = Server.MapPath("~/Content/CorreoCita/PlantillaCorreo.html");

                StreamReader leer = new StreamReader(path);

                body = leer.ReadToEnd();

                body = body.Replace("[*Nombre del Candidato*]", cita.Candidato.Nombre);
                body = body.Replace("[*Nombre de la vacante*]", cita.Candidato.Vacante.Nombre);

                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                string imagePath = Server.MapPath("~/Content/CorreoCita/img/logo.jpg");

                LinkedResource logo = new LinkedResource(imagePath, MediaTypeNames.Image.Jpeg);

                logo.ContentId = "img_logo";

                logo.TransferEncoding = TransferEncoding.Base64;

                avHtml.LinkedResources.Add(logo);

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = puerto,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(correo, password),
                    EnableSsl = true
                };

                var mensaje = new MailMessage
                {
                    From = new MailAddress(correo, "Liliana"),
                    Subject = "Asunto",
                    Body = body,
                    IsBodyHtml = true
                };

                mensaje.To.Add(cita.Candidato.Correo);
                //mensaje.To.Add("ligglez02@gmail.com");

                mensaje.AlternateViews.Add(avHtml);

                smtpClient.Send(mensaje);

            }
            catch (Exception ex)
            {

            }
            return View();
        }



    }
}