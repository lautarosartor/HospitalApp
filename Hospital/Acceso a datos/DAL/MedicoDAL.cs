using Comun.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class MedicoDAL
    {
        //LeerTodo - LeerUno - Crear - Actualiza - Eliminar

        //cantidad de elementos por pagina - numero de pagina - (filtrado) busqueda textual
        public static ListadoPaginadoVMR<MedicoVMR>ObtenerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<MedicoVMR> medicos = new ListadoPaginadoVMR<MedicoVMR>();
            
            return medicos;
        }

        ObtenerUno()
        {

        }

        Actualizar()
        {

        }

        Eliminar()
        {

        }
    }
}
