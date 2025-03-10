using DL_EF;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetByIdMunicipio (int idMunicipio)
        {
            ML.Result result = new ML.Result ();

            using(DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
            {
                try
                {
                    var query = context.ColoniaGetByIdMunicipio(idMunicipio).ToList();

                    if (query.Count() > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Colonia colonia = new ML.Colonia();
                            colonia.Municipio = new ML.Municipio();

                            if (obj.Nombre == null || obj.Nombre.Length == 0)
                            {
                                colonia.IdColonia = 0;
                                colonia.Nombre = "";
                                colonia.Municipio.IdMunicipio = 0;
                            }
                            else
                            {
                                colonia.IdColonia = obj.IdColonia;
                                colonia.Nombre = obj.Nombre;
                            }

                            result.Objects.Add(colonia);
                        }
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
                    }
                } catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                    result.Ex = ex;
                }
            }
            return result;
        }
    }

    
}
