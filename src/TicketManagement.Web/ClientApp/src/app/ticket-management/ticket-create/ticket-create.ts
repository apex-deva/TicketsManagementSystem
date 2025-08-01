import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { TicketService } from '../ticket.service';
import { Router } from '@angular/router';
import { Cities, Districts, Governorates } from '../models/locations.constants';
import { HttpClientModule } from '@angular/common/http';
@Component({
  selector: 'app-ticket-create',
  standalone: true,
 imports: [CommonModule, ReactiveFormsModule, HttpClientModule],
 providers:[TicketService],
  templateUrl: './ticket-create.html',
  styleUrl: './ticket-create.scss'
})
export class TicketCreate {
private fb = inject(FormBuilder);
  private ticketService = inject(TicketService);
  public router = inject(Router);

  // Static location data
  governorates = Governorates;
  cities = Cities;
  districts = Districts;

  // Filtered location options based on selections
  filteredCities: string[] = [];
  filteredDistricts: string[] = [];

  // Form definition
  ticketForm = this.fb.group({
    phoneNumber: ['', [Validators.required, Validators.pattern(/^01[0-9]{9}$/)]],
    governorate: ['', Validators.required],
    city: ['', Validators.required],
    district: ['', Validators.required]
  });

  constructor() {
    // Watch for governorate changes to update cities
    this.ticketForm.get('governorate')?.valueChanges.subscribe(governorate => {
      this.filteredCities = governorate ? this.cities[governorate] || [] : [];
      this.ticketForm.get('city')?.reset();
      this.ticketForm.get('district')?.reset();
    });

    // Watch for city changes to update districts
    this.ticketForm.get('city')?.valueChanges.subscribe(city => {
      const governorate = this.ticketForm.get('governorate')?.value;
      if (governorate && city) {
        this.filteredDistricts = this.districts[governorate]?.[city] || [];
      } else {
        this.filteredDistricts = [];
      }
      this.ticketForm.get('district')?.reset();
    });
  }

  onSubmit() {
    if (this.ticketForm.invalid) {
      this.ticketForm.markAllAsTouched();
      return;
    }

    const formValue = this.ticketForm.value;
    this.ticketService.createTicket({
      phoneNumber: formValue.phoneNumber!,
      governorate: formValue.governorate!,
      city: formValue.city!,
      district: formValue.district!
    }).subscribe({
      next: () => {
        this.router.navigate(['../tickets']);
      },
      error: (err) => {
        console.error('Error creating ticket:', err);
      }
    });
  }
}
