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
    public class EgresoBLL
    {
        /*Basicamente esta clase funcionara como
        conexion entre la capa de presentacion y la capa de acceso a datos*/
        public static ListadoPaginadoVMR<EgresoVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            return EgresoDAL.LeerTodo(cantidad, pagina, textoBusqueda);
        }


        public static EgresoVMR LeerUno(long idEgreso)
        {
            return EgresoDAL.LeerUno(idEgreso);
        }


        public static void Crear(Egreso egreso)
        {
            EgresoDAL.Crear(egreso);
        }


        public static void Actualizar(EgresoVMR egresoVMR)
        {
            EgresoDAL.Actualizar(egresoVMR);
        }


        public static void Eliminar(List<long> idEgresos)
        {
            EgresoDAL.Eliminar(idEgresos);
        }
    }
}
