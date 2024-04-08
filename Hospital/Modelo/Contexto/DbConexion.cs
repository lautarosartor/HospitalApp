using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    public partial class DbConexion : DbContext
    {
        private DbConexion(string context) : base(context)
        {
            //Evitar q los objetos anidados dentro de otros objetos se carguen innecesariamente
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            //Redefinir el tiempo de espera de la BD
            this.Database.CommandTimeout = 900;
        }

        //Metodo q nos facilita la creacion de la conexion
        public static DbConexion Create()
        {
            return new DbConexion("name=DbConexion");
        }
    }
}
