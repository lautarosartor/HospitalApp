using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class EgresoDAL
    {
        public static ListadoPaginadoVMR<EgresoVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<EgresoVMR> egresos = new ListadoPaginadoVMR<EgresoVMR>();

            //Conexion a la BD
            using (var db = DbConexion.Create())
            {
                //Que muestre todos los egresos que no estan dados de baja
                var query = db.Egreso
                .Where(e => !e.borrado)
                .Select(e => new EgresoVMR
                {
                    Id = e.id,
                    IdIngreso = e.id,
                    Fecha = e.fecha,
                    Monto = e.monto,
                });

                //Condicion si se utiliza la busqueda por texto
                /*if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    query = query.Where(e => e.Tratamiento.Contains(textoBusqueda));
                }*/

                //Obtener la cantidad de resultados
                egresos.CantidadTotal = query.Count();

                //Forma en q se mostraran los resultados en la pagina
                egresos.Elemento = query
                .OrderBy(e => e.Id)
                .Skip(pagina * cantidad)
                .Take(cantidad)
                .ToList();
            }

            return egresos;
        }

        public static EgresoVMR LeerUno(long idEgreso)
        {
            EgresoVMR egreso = null;

            //Conexion a la BD
            using (var db = DbConexion.Create())
            {
                egreso = db.Egreso
                .Where(e => e.borrado && e.id == idEgreso)
                .Select(e => new EgresoVMR
                {
                    Id = e.id,
                    IdIngreso = e.id,
                    IdMedico = e.id,
                    Fecha = e.fecha,
                    Tratamiento = e.tratamiento,
                    Monto = e.monto,
                }).FirstOrDefault();
            }

            return egreso;
        }

        public static void Crear(Egreso egreso)
        {
            //Conexion a la BD
            using (var db = DbConexion.Create())
            {
                egreso.borrado = false;
                db.Egreso.Add(egreso);
                db.SaveChanges();
            }
        }

        public static void Actualizar(EgresoVMR egresoVMR)
        {
            //Conexion a la BD
            using (var db = DbConexion.Create())
            {
                //Buscamos el egreso por id
                var egresoUpdate = db.Egreso.Find(egresoVMR.Id);

                egresoUpdate.fecha = egresoVMR.Fecha;
                egresoUpdate.tratamiento = egresoVMR.Tratamiento;
                egresoUpdate.monto = egresoVMR.Monto;
                egresoUpdate.idMedico = egresoVMR.IdMedico;
                egresoUpdate.idIngreso = egresoVMR.IdIngreso;

                //Manera de entity para aplicar cambios a un registro
                db.Entry(egresoUpdate).State = System.Data.Entity.EntityState.Modified;

                //Guardamos los cambios
                db.SaveChanges();
            }
        }

        //Borrado logico
        public static void Eliminar(List<long> idEgresos)
        {
            //Conexion a la BD
            using (var db = DbConexion.Create())
            {
                //Listado
                var egresos = db.Egreso.Where(e => idEgresos.Contains(e.id));

                //Iteramos el listado
                foreach (var egreso in egresos)
                {
                    egreso.borrado = true;
                    //Manera de entity para aplicar cambios a un registro
                    db.Entry(egreso).State = System.Data.Entity.EntityState.Modified;
                }
                //Guardamos los cambios
                db.SaveChanges();
            }
        }
    }
}
