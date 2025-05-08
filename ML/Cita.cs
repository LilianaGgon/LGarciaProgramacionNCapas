using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Cita
    {
        public int IdCita { get; set; }
        public string FechaHora { get; set; }
        public string Url { get; set; }
        public ML.Piso Piso { get; set; }
        public ML.Candidato Candidato { get; set; }
        public ML.EstatusCita EstatusCita { get; set; }
        public List<object> Citas { get; set; }


    }
}
