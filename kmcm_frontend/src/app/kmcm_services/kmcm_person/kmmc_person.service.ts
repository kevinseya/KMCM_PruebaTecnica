import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { Kmcm_person } from '../../kmcm_models/kmcm_models';
import { environment } from '../../../environment/enviroment';
@Injectable({
  providedIn: 'root'
})
export class Kmmc_PersonService {
  private apiUrl = `${environment.apiUrl}/Person`;

  constructor(private http: HttpClient) { }

  // Método para obtener todas las personas
  getPersons(): Observable<Kmcm_person[]> {
    const headers = this.createAuthorizationHeader();
    return this.http.get<Kmcm_person[]>(this.apiUrl, { headers });
  }

  // Método para obtener una persona por su ID
  getPersonById(id: number): Observable<Kmcm_person> {
    const headers = this.createAuthorizationHeader();
    return this.http.get<Kmcm_person>(`${this.apiUrl}/${id}`, { headers });
  }

  // Método para agregar una nueva persona
  addPerson(person: Kmcm_person): Observable<Kmcm_person> {
    const headers = this.createAuthorizationHeader();
    return this.http.post<Kmcm_person>(this.apiUrl, person, { headers });
  }

  // Método para actualizar una persona existente
  updatePerson(id: number, person: Kmcm_person): Observable<void> {
    const headers = this.createAuthorizationHeader();
    return this.http.put<void>(`${this.apiUrl}/${id}`, person, { headers });
  }

  // Método para eliminar una persona por su ID
  deletePerson(id: number): Observable<void> {
    const headers = this.createAuthorizationHeader();
    return this.http.delete<void>(`${this.apiUrl}/${id}`, { headers });
  }

  // Método para obtener personas sin usuario relacionado
  getPersonsWithoutUser(): Observable<Kmcm_person[]> {
    const headers = this.createAuthorizationHeader();
    return this.http.get<Kmcm_person[]>(`${this.apiUrl}/personsWithoutUsers`, { headers });
  }

  // Método para crear el encabezado de autorización
  private createAuthorizationHeader(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }
}
