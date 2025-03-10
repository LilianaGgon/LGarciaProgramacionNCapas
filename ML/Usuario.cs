using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Celular { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public bool Estatus { get; set; }
        public string CURP { get; set; }
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
