using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    var query = context.EstadoGetAll().ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Estado estado = new ML.Estado();

                            if (obj.Nombre == null || obj.Nombre.Length == 0)
                            {
                                estado.IdEstado = 0;
                                estado.Nombre = "";
                            } else
                            {
                                estado.IdEstado = obj.IdEstado;
                                estado.Nombre = obj.Nombre;
                            }

                            result.Objects.Add(estado);
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

        //public static ML.Result GetById(int idUsuario)
        //{
        //    ML.Result result = new ML.Result();

        //    try
        //    {
        //        using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
        //        {
        //            var query = context.DireccionGetByIdUsuario().SingleOrDefault();

        //            if (query != null)
        //            {
        //                ML.Estado estado = new ML.Estado();

        //                if (query.IdEstado != null)
        //                {
        //                    estado.IdEstado = query.IdEstado.Value;
        //                }
        //                else
        //                {
        //                    estado.IdEstado = 0;
        //                }
        //                estado.Nombre = query.NombreEstado;

        //                result.Object = estado;
        //                result.Correct = true;
        //            } else
        //            {
        //                result.Correct = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }
        //    return result;
        //}
    }
}
