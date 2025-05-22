using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class EmpresaController : Controller
    {
        // GET: Empresa
        [HttpGet]
        public ActionResult Empresa()
        {
            ML.Empresa empresa = new ML.Empresa();
            ML.Result result = BL.Empresa.GetAll();
            if (result.Objects != null)
            {
                empresa.Empresas = result.Objects;
            }
            else
            {
                empresa.Empresas = new List<object>();
            }

            return View(empresa);
        }

        [HttpGet]
        public ActionResult FormEmpresa(int? idEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();
            if (idEmpresa != null)
            {
                ML.Result result = BL.Empresa.GetById(idEmpresa.Value);
                empresa = (ML.Empresa)result.Object;
            }

            return View(empresa);
        }

        [HttpPost]
        public ActionResult FormEmpresa(ML.Empresa empresa)
        {
            if (empresa.IdEmpresa == 0)
            {
                ML.Result result = BL.Empresa.Add(empresa);
            }
            else
            {
                ML.Result result = BL.Empresa.Update(empresa);
            }

            return RedirectToAction("Empresa");
        }


        public ActionResult Delete(int? idEmpresa)
        {
            ML.Result result = BL.Empresa.Delete(idEmpresa.Value);

            return RedirectToAction("Empresa");
        }

    }
}