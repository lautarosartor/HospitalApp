import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private readonly url = environment.apiUrl;

  constructor(private httpClient: HttpClient) {}

  //Hacemos la llamada al backend
  LeerTodo(cantidad: number, pagina: number, textoBusqueda: string, entidad: string) {
    let parametros = new HttpParams();

    parametros = parametros.append('cantidad', cantidad);
    parametros = parametros.append('pagina', pagina);
    parametros = parametros.append('textoBusqueda', textoBusqueda);

    return this.httpClient.get(`${this.url}/${entidad}`, { params: parametros })
  }

  Eliminar(id: number[], entidad: string) {
    const option = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      body: id
    };

    return this.httpClient.delete(`${this.url}/${entidad}`, option);
  }

  //Tipo y Entidad hacen referncia a si es (Medico, Paciente, Ingreso, Egreso)
  Crear(tipo: any, entidad: string) {
    return this.httpClient.post(`${this.url}/${entidad}`, tipo);
  }

  LeerUno(id: number, entidad: string) {
    return this.httpClient.get(`${this.url}/${entidad}/${id}`);
  }

  Actualizar(id: number, datos: any, entidad: string) {
    return this.httpClient.put(`${this.url}/${entidad}/${id}`, datos);
  }
}
