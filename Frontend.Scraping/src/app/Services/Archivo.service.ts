import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Archivo } from '../Models/Archivo';
import { environment } from '../../environments/environment';
import { Direccion } from '../Models/Direccion';
import { Contenido } from '../Models/Contenido';

@Injectable({
  providedIn: 'root'
})
export class ArchivoService {

     private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  obtenerArchivos(): Observable<Archivo[]> {
    return this.http.get<{ data: Archivo[], success: boolean }>(`${this.apiUrl}/archivos`).pipe(
      map(response => {
        if (response.success) {
          return response.data;
        } else {
          throw new Error('Error en la respuesta del servidor');
        }
      }),
      catchError(this.manejarError)
    );
  }

  subirArchivo(file: File): Observable<any> {
    const usuario = sessionStorage.getItem('usuario') || 'desconocido';
    
    const formData = new FormData();
    formData.append('usuario', usuario);
    formData.append('file', file);

    return this.http.post(`${this.apiUrl}/scraper-file/`, formData).pipe(
      catchError(this.manejarError)
    );
  }

  obtenerDireccionesPorArchivo(archivoId: number): Observable<Direccion[]> {
    return this.http.get<{ data: Direccion[], success: boolean, message?: string }>(`${this.apiUrl}/archivos/DireccionesByFile/${archivoId}`)
      .pipe(
        map(response => response.data),
        catchError(this.manejarError)
      );
  }
  
  obtenerContenidoPorUrl(idUrl: number): Observable<Contenido> {
    return this.http.get<{ data: Contenido, success: boolean, message?: string }>(`${this.apiUrl}/archivos/ContenidoByUrl/${idUrl}`)
      .pipe(
        map(response => response.data),
        catchError(this.manejarError)
      );
  }

  private manejarError(error: HttpErrorResponse): Observable<never> {
    console.error('Error en la API:', error);
    return throwError(() => new Error('Error al obtener los archivos. Intente nuevamente más tarde.'));
  }
}
