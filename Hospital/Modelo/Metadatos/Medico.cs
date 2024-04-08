using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    [MetadataType(typeof (MedicoMetadata))]
    public partial class Medico
    {

    }

    public class MedicoMetadata
    {
        [Required]
        [StringLength(10)]
        public string Dni { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string ApellidoPaterno { get; set; }

        [StringLength(10)]
        public string ApellidoMaterno { get; set; }

        [Required]
        [StringLength(10)]
        public string Celular { get; set; }

        [Required]
        public bool EsEspecialista { get; set; }

        [Required]
        public bool Habilitado { get; set; }
    }
}
