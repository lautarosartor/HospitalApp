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
        public System.DateTime fecha { get; set; }

        [Required]
        [Range(0, 99999999999.99)]
        public decimal monto { get; set; }

        [Required]
        public long idMedico { get; set; }

        [Required]
        public long idIngreso { get; set; }
    }
}
