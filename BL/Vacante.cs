using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Vacante
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    var select = (from vacante in context.Vacantes
                                  select new
                                  {
                                      //Alias = Valor
                                      IdVacante = vacante.IdVacante,
                                      Nombre = vacante.Nombre,
                                      FechaPublicacion = vacante.FechaPublicacion,
                                      FechaLimite = vacante.FechaLimite,
                                      UrlVacante = vacante.UrlVacante,
                                      IdEstatusVacante = vacante.IdEstatusVacante
                                  }).ToList();

                    if (select.Count > 0)
                    {
                        result.Objects = new List<object>(); //Nueva lista de objetos 
                        foreach (var objBD in select)
                        {
                            ML.Vacante vacante = new ML.Vacante();
                            vacante.EstatusVacante = new ML.EstatusVacante();

                            vacante.IdVacante = objBD.IdVacante;
                            vacante.Nombre = objBD.Nombre;
                            vacante.FechaPublicacion = Convert.ToString(objBD.FechaPublicacion);
                            vacante.FechaLimite = Convert.ToString(objBD.FechaLimite);
                            vacante.UrlVacante = objBD.UrlVacante;
                            vacante.EstatusVacante.IdEstatusVacante = Convert.ToInt32(objBD.IdEstatusVacante);

                            result.Objects.Add(vacante);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }



    }
}
