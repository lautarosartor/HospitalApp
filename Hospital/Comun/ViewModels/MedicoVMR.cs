using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.ViewModels
{
    public class MedicoVMR
    {
        public long Id { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Celular { get; set; }
        public bool EsEspecialista { get; set; }
        public bool Habilitado { get; set; }
    }
}
