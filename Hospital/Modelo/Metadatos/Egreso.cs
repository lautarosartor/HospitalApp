using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    [MetadataType (typeof (EgresoMetadata))]
    public partial class Egreso
    {
    }

    public class EgresoMetadata
    {
        [Required]
        public System.DateTime Fecha { get; set; }

        [Required]
        [Range(0, 99999999999.99)]
        public decimal Monto { get; set; }

        [Required]
        public long IdMedico { get; set; }

        [Required]
        public long IdIngreso { get; set; }
    }
}
