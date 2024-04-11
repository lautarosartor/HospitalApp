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
    public class PacienteBLL
    {
        public static ListadoPaginadoVMR<PacienteVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            return PacienteDAL.LeerTodo(cantidad, pagina, textoBusqueda);
        }


        public static PacienteVMR LeerUno(long idPaciente)
        {
            return PacienteDAL.LeerUno(idPaciente);
        }


        public static long Crear(Paciente paciente)
        {
            return PacienteDAL.Crear(paciente);
        }


        public static void Actualizar(PacienteVMR pacienteVMR)
        {
            PacienteDAL.Actualizar(pacienteVMR);
        }


        public static void Eliminar(List<long> idPacientes)
        {
            PacienteDAL.Eliminar(idPacientes);
        }
    }
}
