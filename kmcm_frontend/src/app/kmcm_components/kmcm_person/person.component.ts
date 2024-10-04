import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PersonService } from "../../services/kmcm_person/person.service";
import { Kmcm_person } from '../../kmcm_models/kmcm_person.model';

@Component({
  selector: 'app-person',
  templateUrl: './person.component.html',
  styleUrls: ['./person.component.css']
})
export class PersonComponent implements OnInit {
  persons: Kmcm_person[] = [];
  selectedPerson: Kmcm_person | null = null;

  constructor(private personService: PersonService, private router: Router) { }

  ngOnInit(): void {
    this.loadPersons();
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
  }

  openCreateModal(): void {
    this.selectedPerson = {
      kmcm_name: '',
      kmcm_lastname: '',
      kmcm_address: '',
      kmcm_phone: '',
      kmcm_birthdate: ''
    };
  }

  closeEditModal(): void {
    this.selectedPerson = null;
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
          console.error('Error deleting person', err);
          alert('Error al eliminar la persona');
        }
      });
    }
  }


}
