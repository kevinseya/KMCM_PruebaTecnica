import { Component } from '@angular/core';
import {Kmcm_person, Kmcm_user} from "../../kmcm_models/kmcm_models";
import {Kmmc_PersonService} from "../../kmcm_services/kmcm_person/kmmc_person.service";
import {Kmmc_UserService} from "../../kmcm_services/kmcm_user/kmmc_user.service";
import {Router} from "@angular/router";
import {forkJoin} from "rxjs";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-kmmc-user',
  templateUrl: './kmmc_user.component.html',
  styleUrls: ['./kmmc_user.component.css']
})
export class Kmmc_UserComponent {
  // Arreglo para almacenar los usuarios
  users: any[] = [];
  // Usuario seleccionado para edición
  selectedUser: Kmcm_user | null = null;
  // Formulario reactivo para los usuarios
  userForm!: FormGroup;
  // Lista de personas
  PersonList: Kmcm_person[] = [];
  // Controla la visibilidad del modal
  openModal: boolean = false;
  // Controla si el modal está en modo editable
  editable: boolean = false;
  // Controla la visibilidad de la contraseña
  passwordVisible: boolean = false;

  // Constructor que inyecta los servicios y el enrutador
  constructor(private fb: FormBuilder, private personService: Kmmc_PersonService, private userService: Kmmc_UserService, private router: Router) { }

  // Método que se ejecuta al inicializar el componente
  ngOnInit(): void {
    // Inicializa el formulario reactivo
    this.userForm = this.fb.group({
      kmcm_username: ['', [Validators.required, Validators.maxLength(10)]],
      kmcm_password: ['', [
        Validators.required,
        Validators.pattern(/^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+={}\[\]|\\;:'",.<>\/?`~]).{8,}$/) // Validación de contraseña
      ]],
      kmcm_person_id: ['', [Validators.required]] // Persona asociada al usuario
    });
    // Carga los usuarios y las personas
    this.loadUsers();
    this.getPersons();
  }

  // Método para cargar los usuarios desde el servicio
  loadUsers(): void {
    this.userService.getUsers().subscribe({
      next: (data) => {
        // Mapea los usuarios recibidos
        this.users = data.map(user => ({
          ...user,
          fullName: '' // Se inicializa el nombre completo
        }));

        // Realiza peticiones para obtener las personas asociadas a los usuarios
        const personRequests = this.users.map(user => this.personService.getPersonById(user.kmcm_person_id));

        // Espera a que todas las peticiones se completen
        forkJoin(personRequests).subscribe(persons => {
          persons.forEach((person, index) => {
            if (person) {
              // Asigna el nombre completo al usuario
              this.users[index].fullName = `${person.kmcm_name} ${person.kmcm_lastname}`;
            }
          });
        });
      },
      error: (err) => {
        // Manejo de errores en la carga de usuarios
        console.error('Error fetching users', err);
      }
    });
  }

  // Método para eliminar un usuario
  deleteUser(id: number | null | undefined): void {
    // Verifica si el ID es válido
    if (id === null || id === undefined) {
      alert('No se puede eliminar un user sin un ID válido.');
      return;
    }
    // Confirma la eliminación
    if (confirm('¿Estás seguro de que deseas eliminar este usuario?')) {
      this.userService.deleteUser(id).subscribe({
        next: () => {
          // Notificación de éxito
          alert('Usuario eliminado con éxito');
          this.loadUsers(); // Recarga la lista de usuarios
        },
        error: (err) => {
          // Manejo de errores en la eliminación
          console.error('Error eliminando usuario', err);
          alert('Error al eliminar el usuario');
        }
      });
    }
  }

  // Método para guardar o actualizar un usuario
  saveUser(): void {
    // Verifica si el formulario es válido
    if (this.userForm.valid) {
      const userData = this.userForm.value; // Obtiene los datos del formulario
      const kmcm_id = this.selectedUser?.kmcm_id; // Obtiene el ID del usuario seleccionado
      const data = { kmcm_id, ...userData }; // Prepara los datos para enviar

      // Si se está editando un usuario existente
      if (this.selectedUser?.kmcm_id) {
        this.userService.updateUser(this.selectedUser.kmcm_id, data).subscribe({
          next: () => {
            // Notificación de éxito
            alert('Usuario actualizado con éxito');
            this.loadUsers(); // Recarga la lista de usuarios
            this.closeEditModal(); // Cierra el modal
          },
          error: (err) => {
            // Manejo de errores en la actualización
            console.error('Error updating user', err);
            alert('Error al actualizar el usuario');
          }
        });
      } else {
        // Si se está creando un nuevo usuario
        this.userService.addUser(userData).subscribe({
          next: () => {
            // Notificación de éxito
            alert('Usuario creado con éxito');
            this.loadUsers(); // Recarga la lista de usuarios
            this.closeEditModal(); // Cierra el modal
          },
          error: (err) => {
            // Manejo de errores en la creación
            console.error('Error creando el usuario', err);
            alert('Error al crear el usuario');
          }
        });
      }
    } else {
      // Notificación de error en el formulario
      alert('El formulario contiene errores, por favor revisa los campos.');
    }
  }

  // Método para cerrar el modal de edición
  closeEditModal(): void {
    this.selectedUser = null; // Reinicia el usuario seleccionado
    this.openModal = false; // Cierra el modal
    this.userForm.reset(); // Reinicia el formulario
    this.passwordVisible = false; // Oculta la contraseña
  }

  // Método para abrir el modal de edición de un usuario
  openEditModal(user: Kmcm_user): void {
    this.selectedUser = { ...user }; // Clona el usuario seleccionado
    this.openModal = true; // Abre el modal
    this.editable = true; // Habilita el modo de edición
    this.userForm.patchValue(this.selectedUser); // Rellena el formulario con los datos del usuario
  }

  // Método para abrir el modal de creación de un nuevo usuario
  openCreateModal(): void {
    this.selectedUser = null; // Reinicia el usuario seleccionado
    this.userForm.reset(); // Reinicia el formulario
    this.openModal = true; // Abre el modal
  }

  // Método para obtener la lista de personas sin usuario asociado
  getPersons() {
    this.personService.getPersonsWithoutUser().subscribe(response => {
      this.PersonList = response; // Asigna la lista de personas
      // Notificaciones según la lista de personas
      if (this.PersonList.length === 0) {
        alert('Todas las personas tienen credenciales.');
      } else {
        alert('Existen personas sin credenciales');
      }
    });
  }

  // Método para alternar la visibilidad de la contraseña
  togglePasswordVisibility(): void {
    this.passwordVisible = !this.passwordVisible; // Cambia el estado de visibilidad
  }
}
