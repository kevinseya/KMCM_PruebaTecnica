import { Component } from '@angular/core';
import {AuthService} from "../../services/kmcm_auth/auth.service";
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  token: string = '';
  name: string = '';
  lastname: string = '';

  constructor(private authService: AuthService, private router: Router) { }
  onLogin() {
    this.authService.login(this.username, this.password).subscribe({
      next: (response) => {
        this.token = response.token;
        this.name = response.name;
        this.lastname = response.lastname;
        const completedName = this.name+" "+this.lastname;
        alert('Inicio de sesión exitoso');
        localStorage.setItem('token', this.token);
        localStorage.setItem('name', completedName);
        this.router.navigate(['/persons'])

      },
      error: (err) => {
        alert('Error al iniciar sesión');
        console.error(err);
      }
    });
  }

}
