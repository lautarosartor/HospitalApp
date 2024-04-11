using Comun.ViewModels;
using Logica_de_Negocio.BLL;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

/*Codificar cada uno de los metodos (Leer - Crear - Actualizar - Eliminar)
Cada uno de estos metodos devolvera una respuesta las cuales seran consumidas
desde el frontend por eso se recomienda estandarizar la forma o estructura de
esa respuesta*/


namespace WebApi.Controllers
{
    //Establecemos los CORS
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PacienteController : ApiController
    {
        [HttpGet]
        public IHttpActionResult LeerTodo(int cantidad = 10, int pagina = 0, string textoBusqueda = null)
        {
            var respuesta = new RespuestaVMR<ListadoPaginadoVMR<PacienteVMR>>();

            try
            {
                respuesta.Datos = PacienteBLL.LeerTodo(cantidad, pagina, textoBusqueda);
            }
            catch (Exception e)
            {
                respuesta.Codigo = HttpStatusCode.InternalServerError;
                respuesta.Datos = null;
                respuesta.Mensajes.Add(e.Message);
                respuesta.Mensajes.Add(e.ToString());
            }

            return Content(respuesta.Codigo, respuesta);
        }


        [HttpGet]
        public IHttpActionResult LeerUno(long id)  //aca pongo id en vez de idPaciente porque asi esta en la BD
        {
            var respuesta = new RespuestaVMR<PacienteVMR>();

            try
            {
                respuesta.Datos = PacienteBLL.LeerUno(id);
            }
            catch (Exception e)
            {
                respuesta.Codigo = HttpStatusCode.InternalServerError;
                respuesta.Datos = null;
                respuesta.Mensajes.Add(e.Message);
                respuesta.Mensajes.Add(e.ToString());
            }

            if (respuesta.Datos == null && respuesta.Mensajes.Count() == 0)
            {
                respuesta.Codigo = HttpStatusCode.NotFound;
                respuesta.Mensajes.Add("Registro no encontrado");
            }

            return Content(respuesta.Codigo, respuesta);
        }


        [HttpPost]
        public IHttpActionResult Crear(Paciente paciente)
        {
            var respuesta = new RespuestaVMR<long?>();  //porque devuelve el ID del nuevo registro

            try
            {
                respuesta.Datos = PacienteBLL.Crear(paciente);
            }
            catch (Exception e)
            {
                respuesta.Codigo = HttpStatusCode.InternalServerError;
                respuesta.Datos = null;
                respuesta.Mensajes.Add(e.Message);
                respuesta.Mensajes.Add(e.ToString());
            }

            return Content(respuesta.Codigo, respuesta);
        }


        [HttpPut]
        public IHttpActionResult Actualizar(long id, PacienteVMR pacienteVMR) //aca pongo id en vez de idMedico porque asi esta en la BD
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                pacienteVMR.Id = id;
                PacienteBLL.Actualizar(pacienteVMR);
                respuesta.Datos = true; //devuelve true si los datos se actualizaron
            }
            catch (Exception e)
            {
                respuesta.Codigo = HttpStatusCode.InternalServerError;
                respuesta.Datos = false;
                respuesta.Mensajes.Add(e.Message);
                respuesta.Mensajes.Add(e.ToString());
            }

            return Content(respuesta.Codigo, respuesta);
        }


        [HttpDelete]
        public IHttpActionResult Eliminar(List<long> idPacientes)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                PacienteBLL.Eliminar(idPacientes);
                respuesta.Datos = true; //devuelve true si los datos se dieron de baja
            }
            catch (Exception e)
            {
                respuesta.Codigo = HttpStatusCode.InternalServerError;
                respuesta.Datos = false;
                respuesta.Mensajes.Add(e.Message);
                respuesta.Mensajes.Add(e.ToString());
            }

            return Content(respuesta.Codigo, respuesta);
        }
    }
}
