import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import { environment } from '../../../environment/enviroment';
@Injectable({
  providedIn: 'root'
})
export class Kmmc_AuthService {
  private apiUrl = `${environment.apiUrl}/Kmcm_controllerAuth/login`;
  constructor(private http: HttpClient) { }

  //Método verifcar las credenciales en el backend
  login(username: string, password: string): Observable<any> {
    return this.http.post(this.apiUrl, {Username: username, Password: password});
  }

  //Método para deslogearse y eliminar los items guardados en el almacenamiento del navegaro
  logout(): Observable<void> {
    return new Observable(observer => {
      localStorage.removeItem('token');
      localStorage.removeItem('name');
      observer.next();
      observer.complete();
    });
  }


}
