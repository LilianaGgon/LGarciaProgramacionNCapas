using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EstatusCita
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    var select = (from estatusCitas in context.EstatusCitas
                                  select new
                                  {
                                      IdEstatusCita = estatusCitas.IdEstatusCita,
                                      Nombre = estatusCitas.Nombre
                                  }).ToList();

                    if(select.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var objBD in select)
                        {
                            ML.EstatusCita estatusCita = new ML.EstatusCita();
                            estatusCita.IdEstatusCita = objBD.IdEstatusCita;
                            estatusCita.Nombre = objBD.Nombre;

                            result.Objects.Add(estatusCita);
                        }
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
                    }
                }
            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
