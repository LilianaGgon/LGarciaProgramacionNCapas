using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Direccion
    {
        public static ML.Result GetByIdEF(int idUsuario)
        {
            ML.Result result = new ML.Result(); //Se crea el modelo result
            try
            {
                using (DL_EF.LGarciaProgramacionNCapasEntities context = new DL_EF.LGarciaProgramacionNCapasEntities()) //Se llama el modelo result 
                {
                    var query = context.DireccionGetByIdUsuario(idUsuario).SingleOrDefault(); //Se pasa el stored procedure y la variable

                    if (query != null)
                    {
                        //Si trajo registros 
                        ML.Direccion direccion = new ML.Direccion();
                        direccion.Colonia = new ML.Colonia();

                        direccion.IdDireccion = query.IdDireccion.Value;
                        
                        result.Object = direccion;
                        result.Correct = true;
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
