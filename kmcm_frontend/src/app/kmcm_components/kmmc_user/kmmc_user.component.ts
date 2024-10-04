import { Component } from '@angular/core';
import {Kmcm_person, Kmcm_user} from "../../kmcm_models/kmcm_models";
import {Kmmc_PersonService} from "../../kmcm_services/kmcm_person/kmmc_person.service";
import {Kmmc_UserService} from "../../kmcm_services/kmcm_user/kmmc_user.service";
import {Router} from "@angular/router";
import {forkJoin} from "rxjs";

@Component({
  selector: 'app-kmmc-user',
  templateUrl: './kmmc_user.component.html',
  styleUrls: ['./kmmc_user.component.css']
})
export class Kmmc_UserComponent {
  users: any[] = [];
  selectedUser: Kmcm_user | null = null;

  constructor(private personService: Kmmc_PersonService, private userService: Kmmc_UserService, private router: Router) { }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.userService.getUsers().subscribe({
      next: (data) => {
        this.users = data.map(user => ({
          ...user,
          fullName: '' // Inicializa el campo fullName
        }));

        // Cargar los nombres de las personas correspondientes
        const personRequests = this.users.map(user => this.personService.getPersonById(user.kmcm_person_id));

        // Utiliza forkJoin para hacer todas las solicitudes simultáneamente
        forkJoin(personRequests).subscribe(persons => {
          persons.forEach((person, index) => {
            if (person) {
              // Combina el nombre y el apellido
              this.users[index].fullName = `${person.kmcm_name} ${person.kmcm_lastname}`; // Ajusta según las propiedades de Kmcm_person
            }
          });
        });
      },
      error: (err) => {
        console.error('Error fetching users', err);
      }
    });
  }


  deleteUser(id: number | null | undefined): void {
    if (id === null || id === undefined) {
      alert('No se puede eliminar un user sin un ID válido.');
      return;
    }
    if (confirm('¿Estás seguro de que deseas eliminar este usuario?')) {
      this.userService.deleteUser(id).subscribe({
        next: () => {
          alert('Usuario eliminado con éxito');
          this.loadUsers();
        },
        error: (err) => {
          console.error('Error eliminando usuario', err);
          alert('Error al eliminar el usuario');
        }
      });
    }
  }


}
