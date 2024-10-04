import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Kmmc_PersonService } from "../../kmcm_services/kmcm_person/kmmc_person.service";
import { Kmmc_UserService } from "../../kmcm_services/kmcm_user/kmmc_user.service";
import { Kmcm_person, Kmcm_user } from '../../kmcm_models/kmcm_models';
import { HttpErrorResponse } from "@angular/common/http";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: 'app-person',
  templateUrl: './kmmc_person.component.html',
  styleUrls: ['./kmmc_person.component.css']
})
export class Kmmc_PersonComponent implements OnInit {
  persons: Kmcm_person[] = []; // Lista de personas
  selectedPerson: Kmcm_person | null = null; // Persona seleccionada
  selectedUser: Kmcm_user | null = null; // Usuario seleccionado
  personForm!: FormGroup; // Formulario para la persona
  editable: boolean = false; // Estado de edición
  modalCreation: boolean = false; // Estado del modal de creación
  modalUser: boolean = false; // Estado del modal de usuario
  passwordVisible: boolean = false; // Estado de visibilidad de la contraseña

  constructor(
    private fb: FormBuilder, // Constructor de formularios
    private personService: Kmmc_PersonService, // Servicio de persona
    private userService: Kmmc_UserService, // Servicio de usuario
  ) {}

  ngOnInit(): void {
    this.personForm = this.fb.group({ // Inicializa el formulario
      kmcm_name: ['', [Validators.required, Validators.maxLength(15)]], // Campo nombre
      kmcm_lastname: ['', [Validators.required, Validators.maxLength(15)]], // Campo apellido
      kmcm_address: ['', [Validators.maxLength(100)]], // Campo dirección
      kmcm_phone: ['', [Validators.required, Validators.maxLength(10), Validators.pattern(/^[0-9]+$/)]], // Campo teléfono
      kmcm_birthdate: ['', Validators.required] // Campo fecha de nacimiento
    });
    this.editable = false; // Establece editable a falso
    // Carga inicial de personas
    this.loadPersons(); // Carga personas al iniciar
  }

  loadPersons(): void {
    this.personService.getPersons().subscribe({ // Llama al servicio para obtener las personas
      next: (data) => {
        this.modalCreation = true; // Abre el modal de creación
        this.modalUser = true; // Abre el modal de usuario
        this.persons = data.map(person => ({ // Mapea los datos de las personas
          ...person,
          kmcm_birthdate: new Date(person.kmcm_birthdate).toISOString().split('T')[0] // Formato YYYY-MM-DD
        }));
      },
      error: (err) => {
        console.error('Error fetching persons', err); // Manejo de errores
      }
    });
  }

  // Abrir modal en modo de edición
  openEditModal(person: Kmcm_person): void {
    this.selectedPerson = { ...person }; // Establece la persona seleccionada
    this.personForm.patchValue(this.selectedPerson); // Rellena el formulario con los datos de la persona
    this.modalCreation = true; // Abre el modal de creación
    this.modalUser = false; // Cierra el modal de usuario
  }

  // Modal de creación
  openCreateModal(): void {
    this.modalCreation = true; // Abre el modal de creación
    this.selectedPerson = { // Resetea la persona seleccionada
      kmcm_name: '',
      kmcm_lastname: '',
      kmcm_address: '',
      kmcm_phone: '',
      kmcm_birthdate: ''
    };
    this.personForm.reset(); // Resetea el formulario
    this.modalUser = false; // Cierra el modal de usuario
  }

  closeEditModal(): void {
    this.modalCreation = true; // Mantiene abierto el modal de creación
    this.passwordVisible = false; // Oculta la contraseña
    this.selectedPerson = null; // Resetea la persona seleccionada
    this.personForm.reset(); // Resetea el formulario
  }

  savePerson(): void {
    if (this.personForm.valid) { // Verifica si el formulario es válido
      const personData = this.personForm.value; // Obtiene los datos del formulario
      const kmcm_id = this.selectedPerson?.kmcm_id; // Obtiene el ID de la persona seleccionada
      const data = { kmcm_id, ...personData }; // Prepara los datos a enviar
      if (this.selectedPerson?.kmcm_id) { // Si se está editando
        this.personService.updatePerson(this.selectedPerson.kmcm_id, data).subscribe({ // Llama al servicio para actualizar
          next: () => {
            alert('Persona actualizada con éxito'); // Mensaje de éxito
            this.loadPersons(); // Recarga las personas
            this.closeEditModal(); // Cierra el modal
          },
          error: (err) => {
            console.error('Error updating person', err); // Manejo de errores
            alert('Error al actualizar la persona'); // Mensaje de error
          }
        });
      } else { // Si se está creando
        this.personService.addPerson(personData).subscribe({ // Llama al servicio para crear
          next: () => {
            alert('Persona creada con éxito'); // Mensaje de éxito
            this.loadPersons(); // Recarga las personas
            this.closeEditModal(); // Cierra el modal
          },
          error: (err) => {
            console.error('Error creando la persona', err); // Manejo de errores
            alert('Error al crear la persona'); // Mensaje de error
          }
        });
      }
    } else {
      alert('El formulario contiene errores, por favor revisa los campos.'); // Mensaje si el formulario es inválido
    }
  }

  deletePerson(id: number | null | undefined): void {
    if (id === null || id === undefined) { // Verifica si el ID es válido
      alert('No se puede eliminar una persona sin un ID válido.'); // Mensaje de error
      return;
    }
    if (confirm('¿Estás seguro de que deseas eliminar esta persona?')) { // Confirmación de eliminación
      this.personService.deletePerson(id).subscribe({ // Llama al servicio para eliminar
        next: () => {
          alert('Persona eliminada con éxito'); // Mensaje de éxito
          this.loadPersons(); // Recarga las personas
        },
        error: (err) => {
          console.error('Error eliminando persona', err); // Manejo de errores
          alert('Error al eliminar la persona'); // Mensaje de error
        }
      });
    }
  }

  closeUserModal(): void {
    this.selectedPerson = null; // Resetea la persona seleccionada
    this.selectedUser = null; // Resetea el usuario seleccionado
    this.modalUser = false; // Cierra el modal de usuario
    this.loadPersons(); // Recarga las personas
  }

  getUserByIdPerson(person: Kmcm_person): void {
    if (!person.kmcm_id) { // Verifica si el ID de la persona es válido
      alert('No se puede encontrar el usuario sin un ID válido de persona.'); // Mensaje de error
      return;
    }
    this.selectedPerson = { ...person }; // Establece la persona seleccionada
    this.userService.getUserByPersonId(person.kmcm_id).subscribe({ // Llama al servicio para obtener el usuario
      next: (user: Kmcm_user) => {
        if (user) {
          this.selectedUser = user; // Establece el usuario seleccionado
          this.editable = true; // Cambia a editable
          this.modalUser = true; // Abre el modal de usuario
          this.modalCreation = false; // Cierra el modal de creación
        } else {
          alert('No existe un usuario para esta persona'); // Mensaje de error
        }
      },
      error: (err: HttpErrorResponse) => {
        if (err.status === 404) { // Manejo de error 404
          this.modalUser = false; // Cierra el modal de usuario
          this.selectedUser = null; // Resetea el usuario seleccionado
          alert('No existe un usuario para esta persona'); // Mensaje de error
        } else {
          alert('Ocurrió un error: ' + err.message); // Mensaje de error
        }
      }
    });
  }

  togglePasswordVisibility(): void {
    this.passwordVisible = !this.passwordVisible; // Cambiar el estado de visibilidad de la contraseña
  }
}
