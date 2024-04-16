import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private httpClient: HttpClient) {}

  //Hacemos la llamada al backend
  LeerTodo(cantidad: number, pagina: number, textoBusqueda: string) {
    let parametros = new HttpParams();

    parametros = parametros.append('cantidad', cantidad);
    parametros = parametros.append('pagina', pagina);
    parametros = parametros.append('textoBusqueda', textoBusqueda);

    return this.httpClient.get('http://localhost:58179/api/Medico', { params: parametros })
  }
}
