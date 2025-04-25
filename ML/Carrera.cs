using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Carrera
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int IdCarrera { get; set; }
        public string Nombre { get; set; }
        public List<object> Carreras { get; set; }

    }
}
