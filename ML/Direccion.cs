using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int IdDireccion { get; set; }

        [DisplayName("Calle")]
        [Required(ErrorMessage = "La calle es obligatoria")]
        [RegularExpression(@"[aA-zZ0-9]{1,50}")]
        public string Calle { get; set; }

        [DisplayName("Numero interior")]
        [RegularExpression(@"/[aA-zZ0-9]{1-20}/")]
        public string NumeroInterior { get; set; }

        [DisplayName("Numero exterior")]
        [Required(ErrorMessage = "El numero exterior es obligatorio")]
        [RegularExpression(@"/[aA-zZ0-9]{1-20}/")]
        public string NumeroExterior { get; set; }

        public ML.Colonia Colonia { get; set; }

    }
}
