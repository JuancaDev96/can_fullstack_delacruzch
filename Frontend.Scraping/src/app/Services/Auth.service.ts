import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { Login } from '../Models/Login';
import { environment } from '../../environments/environment';
import { Usuario } from '../Models/Usuario';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = `${environment.apiUrl}/usuarios`;

  constructor(private http: HttpClient) {}

  iniciarSesion(usuario: Login): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, usuario).pipe(
      tap(respuesta => {
        if (respuesta.data.token) {
          sessionStorage.setItem('token', respuesta.data.token);
          sessionStorage.setItem('usuario', respuesta.data.nombre);
        }
      }),
      catchError(this.manejarError)
    );
  }

  registrarUsuario(usuario: Usuario): Observable<any> {
    return this.http.post<any>(this.apiUrl, usuario).pipe(
      catchError(this.manejarError)
    );
  }

  private manejarError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = 'Ocurrió un error inesperado';
    if (error.status === 400) {
      errorMessage = `Error en la solicitud: ${error.error.message}`;
    }
    console.error(`Backend returned code ${error.status}, body was: ${error.error}`);
    return throwError(errorMessage);
  }
  
}
