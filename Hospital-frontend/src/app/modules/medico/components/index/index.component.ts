import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { HttpService } from '../../../../services/http.service';

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

  constructor(private httpService: HttpService) {}

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
}

/*
  Este componente donde implementaremos el GET de los medicos
  del backend y luego tener el paginado, listado, etc.
  Para poder invocar o llamar a un servicio desde el backend
  vamos a necesitar utilizar llamadas a clientes de web services
  y para ello utilizaremos services.
*/
