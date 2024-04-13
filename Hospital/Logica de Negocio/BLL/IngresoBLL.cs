using Comun.ViewModels;
using Datos.DAL;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica_de_Negocio.BLL
{
    /*Basicamente esta clase funcionara como
    conexion entre la capa de presentacion y la capa de acceso a datos*/
    public class IngresoBLL
    {
        public static ListadoPaginadoVMR<IngresoVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            return IngresoDAL.LeerTodo(cantidad, pagina, textoBusqueda);
        }


        public static IngresoVMR LeerUno(long idIngreso)
        {
            return IngresoDAL.LeerUno(idIngreso);
        }


        public static void Crear(Ingreso ingreso)
        {
            IngresoDAL.Crear(ingreso);
        }


        public static void Actualizar(IngresoVMR ingresoVMR)
        {
            IngresoDAL.Actualizar(ingresoVMR);
        }


        public static void Eliminar(List<long> idPacientes)
        {
            IngresoDAL.Eliminar(idPacientes);
        }
    }
}
