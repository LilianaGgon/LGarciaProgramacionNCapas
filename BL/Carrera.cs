using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Carrera
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    var select = (from carrera in context.Carreras
                                  select new
                                  {
                                      IdCarrera = carrera.IdCarrera,
                                      Nombre = carrera.Nombre
                                  }).ToList();

                    if (select.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var carreraBD in select)
                        {
                            ML.Carrera carrera = new ML.Carrera();

                            carrera.IdCarrera = carreraBD.IdCarrera;
                            carrera.Nombre = carreraBD.Nombre;

                            result.Objects.Add(carrera);
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
