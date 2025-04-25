using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BolsaTrabajo
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities ())
                {
                    var select = (from bolsaTrabajo in context.BolsaTrabajoes
                                  select new
                                  {
                                      IdBolsaTrabajo = bolsaTrabajo.IdBolsaTrabajo,
                                      Nombre = bolsaTrabajo.Nombre
                                  }).ToList();

                    if (select.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var bolsaBD in select)
                        {
                            ML.BolsaTrabajo bolsaTrabajo = new ML.BolsaTrabajo();
                            bolsaTrabajo.IdBolsaTrabajo = bolsaBD.IdBolsaTrabajo;
                            bolsaTrabajo.Nombre = bolsaBD.Nombre;

                            result.Objects.Add(bolsaTrabajo);
                        }
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
