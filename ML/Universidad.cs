using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Universidad
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int IdUniversidad { get; set; }
        public string Nombre { get; set; }
        public List<object> Universidades { get; set; }


    }
}
