import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { HttpService } from '../../../../services/http.service';
import { FormPacienteComponent } from '../form/form-paciente.component';

@Component({
  selector: 'app-index',
  templateUrl: './index-paciente.component.html',
  styleUrl: './index-paciente.component.css'
})
export class IndexPacienteComponent implements OnInit{

  //Columnas a visualizar
  displayedColumns: string[] = ['dni', 'nombre', 'celular', 'acciones']

  //Datos de las columnas
  dataSource = new MatTableDataSource<any>([]);

  cantidadTotal = 0;
  cantidadPorPagina = 10;
  numeroDePagina = 0;
  opcionesDePaginado: number[] = [1, 5, 10, 25, 50, 100];

  textoBusqueda = '';

  entidad = 'Paciente';

  constructor(
    private httpService: HttpService,
    private toastr: ToastrService,
    public dialog: MatDialog,
  ) {}

  ngOnInit(): void {
    this.ObtenerPacientes();
  }

  ObtenerPacientes() {
    this.httpService.LeerTodo(this.cantidadPorPagina, this.numeroDePagina, this.textoBusqueda, this.entidad)
    .subscribe((respuesta: any) => {
      this.dataSource.data = respuesta.Datos.Elemento;  //Datos.Elemento esta en el backend
      this.cantidadTotal = respuesta.Datos.CantidadTotal; //Datos.CantidadTotal esta en el backend
    });
  }

  cambiarPagina(event: any) {
    //Actualizamos las variables
    this.cantidadPorPagina = event.pageSize;
    this.numeroDePagina = event.pageIndex;

    this.ObtenerPacientes();
  }

  EliminarPacientes(idPaciente: number) {
    //Confirmar que se eliminar
    let confirmacion = confirm('¿Estás seguro de eliminar el registro del paciente?');

    if (confirmacion) {
      let idPacientes = [idPaciente];

      //Servicio del backend
      this.httpService.Eliminar(idPacientes, this.entidad)
        .subscribe(() => {
          this.toastr.success('Registro <b>eliminado</b> con éxito', 'Exito');
          this.ObtenerPacientes();
        });
    }
  }

  CrearPaciente() {
    //Abrir ventana modal de crear paciente
    const dialogRef = this.dialog.open(FormPacienteComponent, {
      disableClose: true, //Para no poder cerrar el modal aprentando fuera
      autoFocus: true,
      closeOnNavigation: false,
      position: { top: '100px' },
      width: '700px',
      data: {
        tipo: 'CREAR' //Esto hacemos porque el mismo modal lo utilizaremos para detalles/crear/modificar
      }
    });

    dialogRef.afterClosed()
      .subscribe(respuesta => {
        //Si la respuesta no es undefined es pq se agrego un registro
        if (respuesta === 'guardar') {
          this.toastr.success('Registro <b>agregado</b> correctamente', 'Exito');
          this.ObtenerPacientes();
        }
        else {
          this.toastr.warning('Has <b>cancelado</b> la operación', 'Advertencia');
        }
      });
  }

  dobleClickEnRegistro(idPaciente: number) {
    const dialogRef = this.dialog.open(FormPacienteComponent, {
      position: { top: '100px' },
      width: '700px',
      data: {
        tipo: 'DETALLES', //Esto hacemos porque el mismo modal lo utilizaremos para detalles/crear/modificar
        id: idPaciente
      }
    });

    dialogRef.afterClosed()
      .subscribe(respuesta => {
        if (respuesta === 'editar') {
          this.toastr.success('Registro <b>actualizado</b> correctamente', 'Exito');
          this.ObtenerPacientes();
        }
      });
  }
}
