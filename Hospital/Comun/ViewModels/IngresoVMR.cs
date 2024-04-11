using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.ViewModels
{
    public class IngresoVMR
    {
        public long Id { get; set; }
        public System.DateTime Fecha { get; set; }
        public int NumeroSala { get; set; }
        public int NumeroCama { get; set; }
        public string Diagnostico { get; set; }
        public string Observacion { get; set; }
        public long IdMedico { get; set; }
        public long IdPaciente { get; set; }
    }
}
