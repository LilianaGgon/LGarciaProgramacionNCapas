using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Universidad
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    var select = (from universidad in context.Universidads
                                  select new
                                  {
                                      IdUniversidad = universidad.IdUniversidad,
                                      Nombre = universidad.Nombre
                                  }).ToList();

                    if (select.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var objBD in select)
                        {
                            ML.Universidad universidad = new ML.Universidad();

                            universidad.IdUniversidad = objBD.IdUniversidad;
                            universidad.Nombre = objBD.Nombre;
                            
                            result.Objects.Add(universidad);
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
