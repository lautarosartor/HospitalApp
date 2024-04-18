using Comun.ViewModels;
using Modelo.Modelos;
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
        public static ListadoPaginadoVMR<MedicoVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<MedicoVMR> medicos = new ListadoPaginadoVMR<MedicoVMR>();

            //Conexion a la base de datos
            using (var db = DbConexion.Create())
            {
                //Que muestre todos los medicos que no estan dados de baja logica
                var query = db.Medico
                .Where(m => !m.borrado)
                .Select(m => new MedicoVMR
                {
                    Id = m.id,
                    Dni = m.dni,
                    //Concatenamos el nombre con los dos apellidos (el apellido materno se mostrara si es distinto de null sino solo espacio en blanco "")
                    Nombre = m.nombre + " " + m.apellidoPaterno + (m.apellidoMaterno != null ? (" " + m.apellidoMaterno) : ""),
                    EsEspecialista = m.esEspecialista,
                });

                //Condicion si se utiliza la busqueda por texto
                if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    //Al query anterior le agregamos un query mas para filtrar datos especificos
                    query = query.Where(m => m.Dni.Contains(textoBusqueda) || m.Nombre.Contains(textoBusqueda));
                }

                //Obtener la cantidad de resultados
                medicos.CantidadTotal = query.Count();

                //Forma en q se mostraran los datos en la pagina
                medicos.Elemento = query
                .OrderBy(m => m.Id)
                .Skip(pagina * cantidad)    //Saltea cierta cantidad de datos
                .Take(cantidad)             //La cantidad de datos que mostrara por pagina
                .ToList();
            }

            return medicos;
        }

        //long es como int pero mas largo, uso long pq en la bd lo hice asi
        public static MedicoVMR LeerUno(long idMedico)
        {
            MedicoVMR medico = null;

            //Conexion a la base de datos
            using (var db = DbConexion.Create())
            {
                medico = db.Medico
                .Where(m => !m.borrado && m.id == idMedico)
                .Select(m => new MedicoVMR
                {
                    Id = m.id,
                    Dni = m.dni,
                    Nombre = m.nombre,
                    ApellidoPaterno = m.apellidoPaterno,
                    ApellidoMaterno = m.apellidoMaterno,
                    Celular = m.celular,
                    EsEspecialista = m.esEspecialista,
                    Habilitado = m.habilitado,
                }).FirstOrDefault();
            }

            return medico;
        }

        //devolvemos long osea el PK (ID) que se crea
        public static long Crear(Medico medico)
        {
            //Conexion a la base de datos
            using (var db = DbConexion.Create())
            {
                medico.borrado = false; //Por default el borrado es false
                db.Medico.Add(medico);  //Agregamos un nuevo medico
                db.SaveChanges();       //Guardamos el registro
            }

            //Del mismo item que insertamos, que nos devuelva el id
            return medico.id;
        }

        public static void Actualizar(MedicoVMR medicoVMR)
        {
            //Conexion a la base de datos
            using (var db = DbConexion.Create())
            {
                //Buscamos al medico por id
                var medicoUpdate = db.Medico.Find(medicoVMR.Id);

                medicoUpdate.dni = medicoVMR.Dni;
                medicoUpdate.nombre = medicoVMR.Nombre;
                medicoUpdate.apellidoPaterno = medicoVMR.ApellidoPaterno;
                medicoUpdate.apellidoMaterno = medicoVMR.ApellidoMaterno;
                medicoUpdate.esEspecialista = medicoVMR.EsEspecialista;
                medicoUpdate.habilitado = medicoVMR.Habilitado;

                //Manera de entity para aplicar cambios a un registro
                db.Entry(medicoUpdate).State = System.Data.Entity.EntityState.Modified;

                //Guardamos los cambios
                db.SaveChanges();
            }
        }

        //Borrado logico (update de -> borrado = true)
        public static void Eliminar(List<long> idMedicos)   //Para brorrar por lotes y no 1 por 1
        {
            //Conexion a la base de datos
            using (var db = DbConexion.Create())
            {
                //Aca tendremos todos los medicos que tienen IDs que estan contenidos en el listado de medicos que quiero borrar
                var medicos = db.Medico.Where(m => idMedicos.Contains(m.id));

                //Iteramos el listado
                foreach (var medico in medicos)
                {
                    medico.borrado = true;
                    //Manera de entity para aplicar cambios a un registro
                    db.Entry(medico).State = System.Data.Entity.EntityState.Modified;
                }
                //Guardamos los cambios
                db.SaveChanges();
            }
        }
    }
}
