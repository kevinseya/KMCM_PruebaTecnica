import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { Kmcm_user } from '../../kmcm_models/kmcm_models';
import { environment } from '../../../environment/enviroment';

@Injectable({
  providedIn: 'root'
})
export class Kmmc_UserService {
  private apiUrl = `${environment.apiUrl}/User`;
  constructor(private http: HttpClient) { }

  // Método para obtener todas las personas
  getUsers(): Observable<Kmcm_user[]> {
    const headers = this.createAuthorizationHeader();
    return this.http.get<Kmcm_user[]>(this.apiUrl, { headers });
  }

  // Método para obtener un usuario por su ID de la persona
  getUserByPersonId(id: number): Observable<Kmcm_user> {
    const headers = this.createAuthorizationHeader();
    return this.http.get<Kmcm_user>(`${this.apiUrl}/${id}`, { headers });
  }

  // Método para agregar una nuevo usuario
  addUser(person: Kmcm_user): Observable<Kmcm_user> {
    const headers = this.createAuthorizationHeader();
    return this.http.post<Kmcm_user>(this.apiUrl, person, { headers });
  }

  // Método para actualizar un usuario existente
  updateUser(id: number, person: Kmcm_user): Observable<void> {
    const headers = this.createAuthorizationHeader();
    return this.http.patch<void>(`${this.apiUrl}/${id}`, person, { headers });
  }

  // Método para eliminar un usuario por su ID
  deleteUser(id: number): Observable<void> {
    const headers = this.createAuthorizationHeader();
    return this.http.delete<void>(`${this.apiUrl}/${id}`, { headers });
  }

  // Método para crear el encabezado de autorización
  private createAuthorizationHeader(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  // Método para cambiar la contraseña de un usuario
  changeUserPassword(id: number, newPassword: string, username: string): Observable<void> {
    const headers = this.createAuthorizationHeader();
    return this.http.patch<void>(`${this.apiUrl}/${id}`, { kmcm_id: id, kmcm_password: newPassword, kmcm_username: username  }, { headers });
  }
}
