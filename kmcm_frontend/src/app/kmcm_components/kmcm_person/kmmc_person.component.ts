import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Kmmc_PersonService } from "../../kmcm_services/kmcm_person/kmmc_person.service";
import { Kmmc_UserService } from "../../kmcm_services/kmcm_user/kmmc_user.service";

import { Kmcm_person, Kmcm_user } from '../../kmcm_models/kmcm_models';
import { HttpErrorResponse } from "@angular/common/http";

@Component({
  selector: 'app-person',
  templateUrl: './kmmc_person.component.html',
  styleUrls: ['./kmmc_person.component.css']
})
export class Kmmc_PersonComponent implements OnInit {
  persons: Kmcm_person[] = [];
  selectedPerson: Kmcm_person | null = null;
  selectedUser: Kmcm_user | null = null;
  userFlag: boolean = false;
  editMode: boolean = false;
  editable: boolean = false;
  changingPassword: boolean = false;
  updatingPassword: boolean = false;


  constructor(private personService: Kmmc_PersonService, private userService: Kmmc_UserService, private router: Router) { }

  ngOnInit(): void {
    this.loadPersons();
    this.changingPassword = false;
  }

  isPhoneValid(phone: string): boolean {
    const phonePattern = /^[0-9]{10}$/;
    return phonePattern.test(phone);
  }

  loadPersons(): void {
    this.personService.getPersons().subscribe({
      next: (data) => {
        this.persons = data.map(person => ({
          ...person,
          kmcm_birthdate: new Date(person.kmcm_birthdate).toISOString().split('T')[0] // Formato YYYY-MM-DD
        }));
      },
      error: (err) => {
        console.error('Error fetching persons', err);
      }
    });
  }

  openEditModal(person: Kmcm_person): void {
    this.selectedPerson = { ...person };
    this.userFlag = false; // Asegúrate de que solo el modal de edición esté activo
    this.editMode = true; // Cambia a modo de edición
  }

  openCreateModal(): void {
    this.userFlag = false;
    this.selectedPerson = {
      kmcm_name: '',
      kmcm_lastname: '',
      kmcm_address: '',
      kmcm_phone: '',
      kmcm_birthdate: ''
    };
    this.editMode = true; // Cambia a modo de creación
  }

  closeEditModal(): void {
    this.selectedPerson = null;
    this.editMode = false; // Cierra el modo de edición
  }

  savePerson(): void {
    if (this.selectedPerson) {
      if (this.selectedPerson.kmcm_id) {
        // Update existing person
        this.personService.updatePerson(this.selectedPerson.kmcm_id, this.selectedPerson).subscribe({
          next: () => {
            alert('Persona actualizada con éxito');
            this.loadPersons();
            this.closeEditModal();
          },
          error: (err) => {
            console.error('Error updating person', err);
            alert('Error al actualizar la persona');
          }
        });
      } else {
        this.personService.addPerson(this.selectedPerson).subscribe({
          next: () => {
            alert('Persona creada con éxito');
            this.loadPersons();
            this.closeEditModal();
          },
          error: (err) => {
            console.error('Error creating person', err);
            alert('Error al crear la persona');
          }
        });
      }
    }
  }

  deletePerson(id: number | null | undefined): void {
    if (id === null || id === undefined) {
      alert('No se puede eliminar una persona sin un ID válido.');
      return;
    }
    if (confirm('¿Estás seguro de que deseas eliminar esta persona?')) {
      this.personService.deletePerson(id).subscribe({
        next: () => {
          alert('Persona eliminada con éxito');
          this.loadPersons();
        },
        error: (err) => {
          console.error('Error eliminando persona', err);
          alert('Error al eliminar la persona');
        }
      });
    }
  }

  closeUserModal(): void {
    this.selectedPerson = null;
    this.userFlag = false;
    this.editMode = true;
    this.selectedUser = null;
  }

  openCreateUserModal(): void {
    this.selectedUser = {
      kmcm_username: '',
      kmcm_password: '',
      kmcm_person_id: this.selectedPerson?.kmcm_id
    };

    this.editMode = false;
  }

  saveUser(): void {
    if (this.selectedUser) {
      this.userService.addUser(this.selectedUser).subscribe({
        next: (response) => {
          alert('Usuario creado con éxito');
          this.closeUserModal();
          this.changingPassword = false;
          this.loadPersons();
        },
        error: (err) => {
          console.error('Error creando el usuario', err);
          alert('Ocurrió un error al crear el usuario.');
        }
      });
    }
  }

  getUserByIdPerson(person: Kmcm_person): void {
    this.userFlag = true;
    this.editMode = false;
    this.selectedPerson = { ...person };
    if (this.selectedPerson.kmcm_id === null || this.selectedPerson.kmcm_id === undefined) {
      alert('No se puede encontrar el usuario sin un ID válido de persona');
      return;
    }
    this.userService.getUserByPersonId(this.selectedPerson.kmcm_id).subscribe({
      next: (user: Kmcm_user) => {
        if (user) {
          this.selectedUser = user;
          this.userFlag = true;
          this.editable = true;
          this.changingPassword = true;
          alert('Usuario encontrado');
        }
      },
      error: (err: HttpErrorResponse) => {
        if (err.status === 404) {
          this.userFlag = true;
          this.editable = false
          this.selectedUser = null;
          alert('No existe un usuario para esta persona');
        } else {
          alert('Ocurrió un error: ' + err.message);
        }
      }
    });
  }





}
