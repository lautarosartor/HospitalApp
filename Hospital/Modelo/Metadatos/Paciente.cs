using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    [MetadataType (typeof (PacienteMetadata))]
    public partial class Paciente
    {
    }

    public class PacienteMetadata
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

        [StringLength(50)]
        public string ApellidoMaterno { get; set; }

        [Required]
        [StringLength(250)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(10)]
        public string Celular { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
