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
    public class MedicoController : ApiController
    {
        //Cantidad por defecto sera 10, pagina inicial es 0, texto null
        [HttpGet]
        public IHttpActionResult LeerTodo(int cantidad = 10, int pagina = 0, string textoBusqueda = null)
        {
            var respuesta = new RespuestaVMR<ListadoPaginadoVMR<MedicoVMR>>();

            try
            {
                respuesta.Datos = MedicoBLL.LeerTodo(cantidad, pagina, textoBusqueda);
            }
            catch (Exception e)
            {
                respuesta.Codigo = HttpStatusCode.InternalServerError;
                respuesta.Datos = null;
                respuesta.Mensajes.Add(e.Message);  //Almacenamos los mensajes errores
                respuesta.Mensajes.Add(e.ToString());
            }

            return Content(respuesta.Codigo, respuesta);
        }


        [HttpGet]
        public IHttpActionResult LeerUno(long id)   //aca pongo id en vez de idMedico porque asi esta en la BD
        {
            var respuesta = new RespuestaVMR<MedicoVMR>();

            try
            {
                respuesta.Datos = MedicoBLL.LeerUno(id);
            }
            catch (Exception e)
            {
                respuesta.Codigo = HttpStatusCode.InternalServerError;
                respuesta.Datos = null;
                respuesta.Mensajes.Add(e.Message);  //Almacenamos los mensajes errores
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
        public IHttpActionResult Crear(Medico medico)
        {
            var respuesta = new RespuestaVMR<long?>();   //porque devuelve el ID del nuevo registro

            try
            {
                respuesta.Datos = MedicoBLL.Crear(medico);
            }
            catch (Exception e)
            {
                respuesta.Codigo = HttpStatusCode.InternalServerError;
                respuesta.Datos = null;
                respuesta.Mensajes.Add(e.Message);  //Almacenamos los mensajes errores
                respuesta.Mensajes.Add(e.ToString());
            }

            return Content(respuesta.Codigo, respuesta);
        }


        [HttpPut]
        public IHttpActionResult Actualizar(long id, MedicoVMR medicoVMR)   //aca pongo id en vez de idMedico porque asi esta en la BD
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                medicoVMR.Id = id;
                MedicoBLL.Actualizar(medicoVMR);
                respuesta.Datos = true; //devuelve true si los datos se actualizaron
            }
            catch (Exception e)
            {
                respuesta.Codigo = HttpStatusCode.InternalServerError;
                respuesta.Datos = false;
                respuesta.Mensajes.Add(e.Message);  //Almacenamos los mensajes errores
                respuesta.Mensajes.Add(e.ToString());
            }

            return Content(respuesta.Codigo, respuesta);
        }


        [HttpDelete]
        public IHttpActionResult Eliminar(List<long> idMedicos)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                MedicoBLL.Eliminar(idMedicos);
                respuesta.Datos = true; //devuelve true si los datos se dieron de baja
            }
            catch (Exception e)
            {
                respuesta.Codigo = HttpStatusCode.InternalServerError;
                respuesta.Datos = false;
                respuesta.Mensajes.Add(e.Message);  //Almacenamos los mensajes errores
                respuesta.Mensajes.Add(e.ToString());
            }

            return Content(respuesta.Codigo, respuesta);
        }
    }
}
