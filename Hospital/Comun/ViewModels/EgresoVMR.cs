using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.ViewModels
{
    public class EgresoVMR
    {
        public long Id { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Tratamiento { get; set; }
        public decimal Monto { get; set; }
        public long IdMedico { get; set; }
        public long IdIngreso { get; set; }
    }
}
