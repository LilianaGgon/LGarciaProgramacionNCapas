using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class connection
    {
        public static string ObtenerConexion() //Metodo para obtener conexion
        {
            //Trae la conexion del app.config 
            string conexion = ConfigurationManager.ConnectionStrings["LGarciaProgramacionNCapas"].ToString(); 
            //Retorna la conexion 
            return conexion;
        }
    
    }
}
