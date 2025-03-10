using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Result //En esta clase van todos los mensajes dirigidos al usuario en casos de exito o error
    {
        public bool Correct { get; set; } //Indica si el proceso se hizo de manera correcta o no
                                          //TRUE = Proceso hecho correctamente
                                          //FALSE = Error en el proceso

        public string ErrorMessage { get; set; } //Indica cual es el error especifico
        public Exception Ex { get; set; } //Trae todos los errores a detalle
        public object Object { get; set; } //Muestra solo un registro
        public List<object> Objects { get; set; } //Muestra todos los registros

    }
}
