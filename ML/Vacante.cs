using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Vacante
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int IdVacante { get; set; }
        public string Nombre { get; set; }
        public string FechaPublicacion { get; set; }
        public string FechaLimite { get; set; }
        public string UrlVacante { get; set; }
        public ML.EstatusVacante EstatusVacante { get; set; }
        public List<object> Vacantes { get; set; }


    }
}
