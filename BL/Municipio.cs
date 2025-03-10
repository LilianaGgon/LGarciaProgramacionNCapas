using DL_EF;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result GetByIdEstado(int idEstado)
        {
            ML.Result result = new ML.Result();

            using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
            {
                try
                {
                    var query = context.MunicipioGetByIdEstado(idEstado).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Municipio municipio = new ML.Municipio();
                            municipio.Estado = new ML.Estado();

                            if (obj.Nombre == null || obj.Nombre.Length == 0)
                            {
                                municipio.IdMunicipio = 0;
                                municipio.Nombre = "";
                                municipio.Estado.IdEstado = 0;
                            }
                            else
                            {
                                municipio.IdMunicipio = obj.IdMunicipio;
                                municipio.Nombre = obj.Nombre;
                            }

                            result.Objects.Add(municipio);
                        }
                        result.Correct = true;
                    } else
                    {
                        result.Correct= false;
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
