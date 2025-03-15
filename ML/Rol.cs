using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Rol
    {
        [DisplayName("Rol")]
        [Required(ErrorMessage = "El rol es obligatorio")]
        [RegularExpression(@"/[0-6]/")]
        public int IdRol { get; set; }

        public string Nombre { get; set; }

        public List<object> Roles { get; set; }

    }
}
