using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class PacienteDAL
    {
        //cantidad de elementos por pagina - numero de pagina - busqueda por texto
        public static ListadoPaginadoVMR<PacienteVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<PacienteVMR> pacientes = new ListadoPaginadoVMR<PacienteVMR>();

            //Conexion a la BD
            using (var db = DbConexion.Create())
            {
                //Mostramos todos los pacientes que no estan dados de baja logica
                var query = db.Paciente
                .Where(p => !p.borrado)
                .Select(p => new PacienteVMR
                {
                    Id = p.id,
                    Dni = p.dni,
                    //Concatenamos nombre y apellidos (en caso de que tenga materno lo concatena tambien)
                    Nombre = p.nombre + " " + p.apellidoPaterno + (p.apellidoMaterno != null ? (" " + p.apellidoMaterno) : ""),
                    Celular = p.celular
                });

                //Condicion si se utiliza la busqueda por texto
                if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    //Al query anterior le agregamos un query mas para filtrar datos especificos
                    query = query.Where(p => p.Dni.Contains(textoBusqueda) || p.Nombre.Contains(textoBusqueda));
                }

                //Obtener la cantidad de resultados
                pacientes.CantidadTotal = query.Count();

                //Forma en la q se mostraran los datos en la pagina
                pacientes.Elemento = query
                .OrderBy(p => p.Id)
                .Skip(pagina * cantidad)    //Saltea cierta cantidad de datos por pagina
                .Take(cantidad)             //Cantidad de datos que muestra por pagina
                .ToList();
            }

            return pacientes;
        }

        public static PacienteVMR LeerUno(long idPaciente)
        {
            PacienteVMR paciente = null;

            //Conexion a la BD
            using (var db = DbConexion.Create())
            {
                paciente = db.Paciente
                .Where(p => !p.borrado && p.id == idPaciente)
                .Select(p => new PacienteVMR
                {
                    Id = p.id,
                    Dni = p.dni,
                    Nombre = p.nombre,
                    ApellidoPaterno = p.apellidoPaterno,
                    ApellidoMaterno = p.apellidoMaterno,
                    Direccion = p.direccion,
                    Celular = p.celular,
                    Email = p.email
                }).FirstOrDefault();
            }

            return paciente;
        }

        //devolvemos long osea el PK (ID) que se crea
        public static long Crear(Paciente paciente)
        {
            //Conexion a la BD
            using (var db = DbConexion.Create())
            {
                paciente.borrado = false;   //Por default es false
                db.Paciente.Add(paciente);  //Agregamos un nuevo paciente
                db.SaveChanges();           //Guardamos el registro
            }

            return paciente.id;
        }

        public static void Actualizar(PacienteVMR pacienteVMR)
        {
            //Conexion a la BD
            using (var db = DbConexion.Create())
            {
                //Buscamos al paciente por id
                var pacienteUpdate = db.Paciente.Find(pacienteVMR.Id);

                pacienteUpdate.dni = pacienteVMR.Dni;
                pacienteUpdate.nombre = pacienteVMR.Nombre;
                pacienteUpdate.apellidoPaterno = pacienteVMR.ApellidoPaterno;
                pacienteUpdate.apellidoMaterno = pacienteVMR.ApellidoMaterno;
                pacienteUpdate.direccion = pacienteVMR.Direccion;
                pacienteUpdate.celular = pacienteVMR.Celular;
                pacienteUpdate.email = pacienteVMR.Email;

                //Manera de entity para aplicar cambios a un registro
                db.Entry(pacienteUpdate).State = System.Data.Entity.EntityState.Modified;

                //Guardamos los cambios
                db.SaveChanges();
            }
        }

        //Borrado logico (update de -> borrado = true)
        public static void Eliminar(List<long> idPacientes)  //Para borrar por lotes y no 1 por 1
        {
            //Conexion a la BD
            using (var db = DbConexion.Create())
            {
                //Aca tendremos todos los pacientes que tienen IDs que estan contenidos en el listado
                var pacientes = db.Paciente.Where(p => idPacientes.Contains(p.id));

                //Iteramos el listado
                foreach (var paciente in pacientes)
                {
                    paciente.borrado = true;
                    //Manera de entity para aplicar cambios a un registro
                    db.Entry(paciente).State = System.Data.Entity.EntityState.Modified;
                }
                //Guardamos los cambios
                db.SaveChanges();
            }
        }
    }
}
