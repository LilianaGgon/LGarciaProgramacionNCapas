using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Candidato
    {
        public int IdCandidato { get; set; }

        [DisplayName ("Nombre del candidato")]
        [Required (ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression(@"^[a-z A-Z]{1,50}$", ErrorMessage = "Solo se aceptan letras y no puede contener más de 50 caracteres")]
        public string Nombre { get; set; }

        [DisplayName("Apellido paterno del candidato")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression(@"^[a-z A-Z]{1,50}$", ErrorMessage = "Solo se aceptan letras y no puede contener más de 50 caracteres")]
        public string ApellidoPaterno { get; set; }

        [DisplayName("Apellido materno del candidato")]
        [RegularExpression(@"^[a-z A-Z]{1,50}$", ErrorMessage = "Solo se aceptan letras y no puede contener más de 50 caracteres")]
        public string ApellidoMaterno { get; set; }

        [DisplayName("Edad")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression(@"[0-9]{1,2}", ErrorMessage = "Solo se aceptan numeros y no puede contener más de 10 caracteres")]
        public string Edad { get; set; }

        [DisplayName("Correo")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$", ErrorMessage = "ejemplo@email.com \n No puede contener más de 50 caracteres")]
        public string Correo { get; set; }

        [DisplayName("Telefono")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression(@"[0-9]{1,20}", ErrorMessage = "Solo se aceptan numeros y no puede contener más de 20 caracteres")]
        public string Telefono { get; set; }

        [DisplayName("Direccion")]
        [Required(ErrorMessage = "La direccion es obligatorio")]
        [RegularExpression(@"[aA-zZ0-9.-]{15,500}", ErrorMessage = "Solo se aceptan letras, numeros, -. \n No puede contener más de 500 caracteres")]
        public string Direccion { get; set; }

        [DisplayName("Foto")]
        public byte[] Foto { get; set; }
        public string FotoBase64 { get; set; }

        [DisplayName("Curriculum")]
        public byte[] Curriculum { get; set; }
        public string CurriculumBase64 { get; set; }

        // propiedad de navegacion universidad
        public ML.Universidad Universidad { get; set; }

        // propiedad de navegacion carrera
        public ML.Carrera Carrera { get; set; }

        // propiedad de navegacion bolsa de trabajo
        public ML.BolsaTrabajo BolsaTrabajo { get; set; }

        // propiedad de navegacion vacante
        public ML.Vacante Vacante { get; set; }

        public List<object> Candidatos { get; set; }

    }
}
