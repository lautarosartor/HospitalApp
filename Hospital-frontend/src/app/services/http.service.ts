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
  LeerTodo(cantidad: number, pagina: number, textoBusqueda: string) {
    let parametros = new HttpParams();

    parametros = parametros.append('cantidad', cantidad);
    parametros = parametros.append('pagina', pagina);
    parametros = parametros.append('textoBusqueda', textoBusqueda);

    return this.httpClient.get(`${this.url}/Medico`, { params: parametros })
  }

  Eliminar(idMedicos: number[]) {
    const option = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      body: idMedicos
    };

    return this.httpClient.delete(`${this.url}/Medico`, option);
  }

  Crear(medico: any) {
    return this.httpClient.post(`${this.url}/Medico`, medico);
  }

  LeerUno(idMedico: number) {
    return this.httpClient.get(`${this.url}/Medico/${idMedico}`);
  }

  Actualizar(idMedico: number, dataMedico: any) {
    return this.httpClient.put(`${this.url}/Medico/${idMedico}`, dataMedico);
  }
}
