import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { HttpService } from '../../../../services/http.service';
import { FormComponent } from '../form/form.component';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrl: './index.component.css'
})
export class IndexComponent implements OnInit{

  //Columnas a visualizar
  displayedColumns: string[] = ['dni', 'nombre', 'esEspecialista', 'acciones'];

  //Datos de las columnas
  dataSource = new MatTableDataSource<any>([]);

  cantidadTotal = 0;
  cantidadPorPagina = 10;
  numeroDePagina = 0;
  opcionesDePaginado: number[] = [1, 5, 10, 25, 50, 100]

  textoBusqueda = '';

  constructor(
    private httpService: HttpService,
    private toastr: ToastrService,
    public dialog: MatDialog,
  ) {}

  ngOnInit(): void {
    this.LeerTodo();
  }

  LeerTodo() {
    //Le pasamos parametros por defecto en este caso
    this.httpService.LeerTodo(this.cantidadPorPagina, this.numeroDePagina, this.textoBusqueda) //Esto devuelve un observable
      .subscribe((respuesta: any) => {
        this.dataSource.data = respuesta.Datos.Elemento;  //Datos.Elemento esta en el backend
        this.cantidadTotal = respuesta.Datos.CantidadTotal; //Datos.CantidadTotal esta en el backend
      });
  }

  cambiarPagina(event: any) {
    //Actualizamos las variables
    this.cantidadPorPagina = event.pageSize;
    this.numeroDePagina = event.pageIndex;

    this.LeerTodo();
  }

  Eliminar(idMedico: number) {
    //Confirmar que se eliminar
    let confirmacion = confirm('¿Estás seguro de eliminar el registro?');

    if (confirmacion) {
      let idMedicos = [idMedico];

      //Servicio del backend
      this.httpService.Eliminar(idMedicos) //Esto devuelve un observable
        .subscribe(() => {
          this.toastr.success('Registro <b>eliminado</b> con éxito', 'Exito');
          this.LeerTodo();
        });
    }
  }

  Crear() {
    //Abrir ventana modal de crear medico
    const dialogRef = this.dialog.open(FormComponent, {
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
        //Si la respuesta no es unefined es pq se agrego un registro
        if (respuesta === 'guardar') {
          this.toastr.success('Registro <b>agregado</b> correctamente', 'Exito');
          this.LeerTodo();  //Actualizamos la lista
        }
        else {
          this.toastr.warning('Has <b>cancelado</b> la operación', 'Advertencia');
        }
      });
  }

  dobleClickEnRegistro(idMedico: number) {
    //console.log(idMedico);
    //Abrir ventana modal de detalle medico
    const dialogRef = this.dialog.open(FormComponent, {
      position: { top: '100px' },
      width: '700px',
      data: {
        tipo: 'DETALLES', //Esto hacemos porque el mismo modal lo utilizaremos para detalles/crear/modificar
        id: idMedico
      }
    });

    dialogRef.afterClosed()
      .subscribe(respuesta => {
        if (respuesta === 'editar') {
          this.toastr.success('Registro <b>actualizado</b> correctamente', 'Exito');
          this.LeerTodo();  //Actualizamos la lista
        }
      });
  }
}

/*
  Este componente donde implementaremos el GET de los medicos
  del backend y luego tener el paginado, listado, etc.
  Para poder invocar o llamar a un servicio desde el backend
  vamos a necesitar utilizar llamadas a clientes de web services
  y para ello utilizaremos services.
*/
