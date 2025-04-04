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
        [RegularExpression(@"[a-z A-Z 0-9]{1,50}", ErrorMessage = "Solo se aceptan letras y números. \nNo puede contener más de 50 caracteres")]
        public string Calle { get; set; }

        [DisplayName("Numero interior")]
        [RegularExpression(@"[a-z A-Z 0-9]{1,20}", ErrorMessage = "Solo se aceptan letras y números. \nNo puede contener más de 20 caracteres")]
        public string NumeroInterior { get; set; }

        [DisplayName("Numero exterior")]
        [Required(ErrorMessage = "El numero exterior es obligatorio" )]
        [RegularExpression(@"[a-z A-Z 0-9]{1,20}", ErrorMessage = "Solo se aceptan letras y números. \nNo puede contener más de 20 caracteres")]
        public string NumeroExterior { get; set; }

        public ML.Colonia Colonia { get; set; }

    }
}
