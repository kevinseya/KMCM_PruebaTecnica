import { Component } from '@angular/core';
import {Kmmc_AuthService} from "../../kmcm_services/kmcm_auth/kmmc_auth.service";
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './kmmc_login.component.html',
  styleUrls: ['./kmmc_login.component.css']
})
export class Kmmc_LoginComponent {
  username: string = '';
  password: string = '';
  token: string = '';
  name: string = '';
  lastname: string = '';

  constructor(private authService: Kmmc_AuthService, private router: Router) { }
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
        this.router.navigate(['/menu'])

      },
      error: (err) => {
        alert('Error al iniciar sesión');
        console.error(err);
      }
    });
  }

}
