import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7240/kmcm_api/Kmcm_controllerAuth/login';
  constructor(private http: HttpClient) { }
  login(username: string, password: string): Observable<any> {
    return this.http.post(this.apiUrl, {Username: username, Password: password});
  }

  logout(): Observable<void> {
    return new Observable(observer => {
      localStorage.removeItem('token');
      localStorage.removeItem('name');
      observer.next();
      observer.complete();
    });
  }


}
