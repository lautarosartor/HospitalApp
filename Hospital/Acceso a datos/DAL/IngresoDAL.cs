using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class IngresoDAL
    {
        //cantidad de elementos por pagina - numero de pagina - busqueda por texto
        public static ListadoPaginadoVMR<IngresoVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<IngresoVMR> ingresos = new ListadoPaginadoVMR<IngresoVMR>();

            //Conexion a la BD
            using (var db = DbConexion.Create())
            {
                //Que muestre todos los ingresos que no estan dados de baja
                var query = db.Ingreso
                .Where(i => !i.borrado)
                .Select(i => new IngresoVMR
                {
                    Id = i.id,
                    IdPaciente = i.idPaciente,
                    Fecha = i.fecha,
                    NumeroSala = i.numeroSala,
                    NumeroCama = i.numeroCama,
                    Diagnostico = i.diagnostico,
                });

                //Condicion si se utiliza la busqueda por texto
                /*if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    //Al query anterior le agregamos un query mas para filtrar mas precisamente
                    query = query.Where(i => i.Diagnostico.Contains(textoBusqueda));
                }*/

                //Obtener la cantidad de resultados
                ingresos.CantidadTotal = query.Count();

                //Forma en q se mostraran los datos en la pagina
                ingresos.Elemento = query
                .OrderBy(i => i.Id)
                .Skip(pagina * cantidad)
                .Take(cantidad)
                .ToList();
            }

            return ingresos;
        }

        public static IngresoVMR LeerUno(long idIngreso)
        {
            IngresoVMR ingreso = null;

            //Conexion a la BD
            using (var db = DbConexion.Create())
            {
                ingreso = db.Ingreso
                .Where(i => !i.borrado && i.id == idIngreso)
                .Select(i => new IngresoVMR
                {
                    Id = i.id,
                    Fecha = i.fecha,
                    NumeroSala = i.numeroSala,
                    NumeroCama = i.numeroCama,
                    Diagnostico = i.diagnostico,
                    Observacion = i.observacion,
                    IdMedico = i.idMedico,
                    IdPaciente = i.idPaciente,
                }).FirstOrDefault();
            }

            return ingreso;
        }

        public static void Crear(Ingreso ingreso)
        {
            //Conexion a la BD
            using (var db = DbConexion.Create())
            {
                ingreso.borrado = false;
                db.Ingreso.Add(ingreso);
                db.SaveChanges();
            }
        }

        public static void Actualizar(IngresoVMR ingresoVMR)
        {
            //Conexion a la BD
            using (var db = DbConexion.Create())
            {
                //Buscamos el ingreso por id
                var ingresoUpdate = db.Ingreso.Find(ingresoVMR.Id);

                ingresoUpdate.numeroSala = ingresoVMR.NumeroSala;
                ingresoUpdate.numeroCama = ingresoVMR.NumeroCama;
                ingresoUpdate.diagnostico = ingresoVMR.Diagnostico;
                ingresoUpdate.observacion = ingresoVMR.Observacion;
                ingresoUpdate.idMedico = ingresoVMR.IdMedico;

                //Manera de entity para aplicar cambios a un registro
                db.Entry(ingresoUpdate).State = System.Data.Entity.EntityState.Modified;

                //Guardamos los cambios
                db.SaveChanges();
            }
        }

        //Borrado logico (update de -> borrado = true)
        public static void Eliminar(List<long> idIngresos)  //Para borrar muchos a la vez
        {
            //Conexion a la BD
            using (var db = DbConexion.Create())
            {
                //Aca tendremos el listado de los IDs
                var ingresos = db.Ingreso.Where(i => idIngresos.Contains(i.id));

                //Iteramos el listado
                foreach (var ingreso in ingresos)
                {
                    ingreso.borrado = true;
                    //Manera de entity para aplicar cambios a un registro
                    db.Entry(ingreso).State = System.Data.Entity.EntityState.Modified;
                }
                //Guardamos los cambios
                db.SaveChanges();
            }
        }
    }
}
