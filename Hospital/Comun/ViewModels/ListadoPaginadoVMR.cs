using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.ViewModels
{
    public class ListadoPaginadoVMR<Tipo>
    {
        //Cantidad total de filas
        public int CantidadTotal { get; set; }

        //Cantidad de elementos en lista
        public IEnumerable<Tipo> Elemento { get; set;}
    
        public ListadoPaginadoVMR()
        {
            Elemento = new List<Tipo>();
            CantidadTotal = 0;
        }
    }
}
