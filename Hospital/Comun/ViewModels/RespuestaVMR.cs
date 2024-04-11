using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Comun.ViewModels
{
    public class RespuestaVMR<Tipo>
    {
        //Definir un codigo de respuesta para saber si fue exitosa
        public HttpStatusCode Codigo { get; set; }

        //Informacion que yo voy a devolver
        public Tipo Datos { get; set; }

        //Listado de mensajes
        public List<string> Mensajes { get; set; }

        public RespuestaVMR()
        {
            Codigo = HttpStatusCode.OK;
            Datos = default(Tipo);
            Mensajes = new List<string>();
        }
    }
}
