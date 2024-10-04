import { Component, OnInit } from '@angular/core';
import { Kmmc_AuthService } from '../../kmcm_services/kmcm_auth/kmmc_auth.service'; // Asegúrate de tener este servicio
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './kmmc_navbar.component.html',
  styleUrls: ['./kmmc_navbar.component.css']
})
export class Kmmc_NavbarComponent implements OnInit {
  userName: string | null = null;
  constructor(private authService: Kmmc_AuthService, private router: Router) { }

  ngOnInit(): void {
    this.userName  = localStorage.getItem('name'); // Cambia esto según tu implementación
  }

  logout(): void {
    this.authService.logout().subscribe({
      next: () => {
        alert('Sesión cerrada con éxito');
        this.router.navigate(['/login']); // Redirige al login después de cerrar sesión
      },
      error: (err) => {
        console.error('Error al cerrar sesión', err);
        alert('Error al cerrar sesión');
      }
    });
  }
}
