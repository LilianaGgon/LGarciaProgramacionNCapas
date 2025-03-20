using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [DisplayName ("Nombre")]
        [Required (ErrorMessage = "Nombre es obligatorio")]
        [RegularExpression(@"^[a-z A-Z]{1,50}$", ErrorMessage = "Solo se aceptan letras y no puede contener más de 50 caracteres")]
        public string Nombre { get; set; }

        [DisplayName("Apellido Paterno")]
        [Required(ErrorMessage = "Apellido paterno es obligatorio")]
        [RegularExpression(@"^[a-z A-Z]{1,50}$", ErrorMessage = "Solo se aceptan letras y no puede contener más de 50 caracteres")]
        public string ApellidoPaterno { get; set; }

        [DisplayName("Apellido Materno")]
        [RegularExpression(@"^[a-z A-Z]{1,50}$", ErrorMessage = "Solo se aceptan letras y no puede contener más de 50 caracteres")]
        public string ApellidoMaterno { get; set; }

        [DisplayName("Celular")]
        [RegularExpression(@"[0-9]{1,20}", ErrorMessage = "Solo se aceptan numeros y no puede contener más de 20 caracteres")]
        public string Celular { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "El username es obligatorio")]
        [RegularExpression(@"[aA-zZ0-9_.-]{1,50}", ErrorMessage = "Solo se aceptan letras, numeros, -._" +
            "No puede contener más de 50 caracteres")]
        public string UserName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "El email es obligatorio")]
        [RegularExpression(@"^[^\s@@]+@@[^\s@@]+\.[^\s@@]+$", ErrorMessage = "ejemplo1@email.com " +
            "No puede contener más de 50 caracteres")]
        public string Email { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "El password es obligatorio y debe tener minimo 8 caracteres")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,50}$")]
        public string Password { get; set; }

        [DisplayName("Fecha de nacimiento")]
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [RegularExpression(@"(19[0-9]{2}|2[0-9]{3})/(0[1-9]|1[012])/([123]0|[012][1-9]|31)", ErrorMessage = "El formato de la fecha de nacimiento es aaaa/mm/dd")]
        public string FechaNacimiento { get; set; }

        [DisplayName("Sexo")]
        [Required(ErrorMessage = "El sexo es obligatorio")]
        [RegularExpression(@"^(m|h|M|H){1}$")]
        public string Sexo { get; set; }

        [DisplayName("Telefono")]
        [Required(ErrorMessage = "El telefono es obligatorio")]
        [RegularExpression(@"^(0-9){1,20}$", ErrorMessage = "Solo se aceptan numeros" +
            "No puede contener más de 50 caracteres")]
        public string Telefono { get; set; }

        [DisplayName("Estatus")]
        //[Required(ErrorMessage = "El estatus es obligatorio")]
        //[RegularExpression(@"/[True-False]{1}/")]
        public bool Estatus { get; set; }

        [DisplayName("CURP")]
        [RegularExpression(@"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$", ErrorMessage = "El formato del CURP es AAAA000000AAAAAAA0")]
        public string CURP { get; set; }

        [DisplayName("Imagen")]
        public byte[] Imagen { get; set; }

        public ML.Rol Rol { get; set; } //Propiedad de navegacion
        public List<object> Usuarios { get; set; } //Propiedad que se va a retornar en la vista del MVC
        public ML.Direccion Direccion { get; set; }

    }

    public class UsuarioSqlClient
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int Edad { get; set; }
        
    }
}
