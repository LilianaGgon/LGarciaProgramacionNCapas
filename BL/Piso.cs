using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Piso
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities ())
                {
                    var select = (from pisos in context.Pisoes
                                  select new
                                  {
                                      IdPiso = pisos.IdPiso,
                                      Nombre = pisos.Nombre
                                  }).ToList ();

                    if (select.Count > 0 )
                    {
                        result.Objects = new List<object>();
                        foreach (var objBD in select)
                        {
                            ML.Piso piso = new ML.Piso ();
                            piso.IdPiso = objBD.IdPiso;
                            piso.Nombre = objBD.Nombre;

                            result.Objects.Add(piso);
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
