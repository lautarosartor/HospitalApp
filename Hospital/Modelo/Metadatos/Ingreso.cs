using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    [MetadataType (typeof (IngresoMetada))]
    public partial class Ingreso
    {
    }

    public class IngresoMetada
    {
        [Required]
        public System.DateTime Fecha { get; set; }

        [Required]
        public int NumeroSala { get; set; }

        [Required]
        public int NumeroCama { get; set; }

        [Required]
        public string Diagnostico { get; set; }

        [Required]
        public long IdMedico { get; set; }

        [Required]
        public long IdPaciente { get; set; }
    }
}
