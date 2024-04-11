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
    public class MedicoBLL
    {
        public static ListadoPaginadoVMR<MedicoVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            return MedicoDAL.LeerTodo(cantidad, pagina, textoBusqueda);
        }

        public static MedicoVMR LeerUno(long idMedico)
        {
            return MedicoDAL.LeerUno(idMedico);
        }

        public static long Crear(Medico medico)
        {
            return MedicoDAL.Crear(medico);
        }

        public static void Actualizar(MedicoVMR medicoVMR)
        {
            MedicoDAL.Actualizar(medicoVMR);
        }

        public static void Eliminar(List<long> idMedicos)
        {
            MedicoDAL.Eliminar(idMedicos);
        }
    }
}
