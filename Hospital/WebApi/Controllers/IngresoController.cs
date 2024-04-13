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
    public class IngresoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult LeerTodo(int cantidad = 10, int pagina = 0, string textoBusqueda = null)
        {
            var respuesta = new RespuestaVMR<ListadoPaginadoVMR<IngresoVMR>>();

            try
            {
                respuesta.Datos = IngresoBLL.LeerTodo(cantidad, pagina, textoBusqueda);
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
        public IHttpActionResult LeerUno(long id)
        {
            var respuesta = new RespuestaVMR<IngresoVMR>();

            try
            {
                respuesta.Datos = IngresoBLL.LeerUno(id);
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
        public IHttpActionResult Crear(Ingreso ingreso)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                IngresoBLL.Crear(ingreso);
                respuesta.Datos = true;
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


        [HttpPut]
        public IHttpActionResult Actualizar(long id, IngresoVMR ingresoVMR)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                ingresoVMR.Id = id;
                IngresoBLL.Actualizar(ingresoVMR);
                respuesta.Datos = true;
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
        public IHttpActionResult Eliminar(List<long> idIngresos)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                IngresoBLL.Eliminar(idIngresos);
                respuesta.Datos = true;
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
