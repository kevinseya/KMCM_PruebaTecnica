import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/kmcm_auth/auth.service'; // Asegúrate de tener este servicio
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  userName: string | null = null;
  constructor(private authService: AuthService, private router: Router) { }

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
