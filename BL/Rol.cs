using DL_EF;
using ML;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities())
                {
                    var select = (from roles in context.Rols
                                  select new
                                  {
                                      IdRol = roles.IdRol,
                                      Nombre = roles.Nombre
                                  }).ToList();

                    if (select.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var objBD in select)
                        {
                            ML.Rol rol = new ML.Rol();

                            if (objBD.IdRol != null)
                            {
                                rol.IdRol = Convert.ToInt32(objBD.IdRol);
                            }
                            else
                            {
                                rol.IdRol = 0;
                            }
                            rol.Nombre = objBD.Nombre;

                            result.Objects.Add(rol);
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
