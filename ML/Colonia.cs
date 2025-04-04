using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Colonia
    {
        [DisplayName("Id de la colonia")]
        [Required(ErrorMessage = "La colonia es obligatoria")]
        public int IdColonia { get; set; }
        public string Nombre { get; set; }
        public string CodigoPostal { get; set; }
        public ML.Municipio Municipio { get; set; } //Propiedad de navegacion
        public List<object> Colonias { get; set; }

    }
}
