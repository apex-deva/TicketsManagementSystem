import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'tickets',
    pathMatch: 'full'
  },
     {
    path: 'tickets',
    loadComponent: () => import('./ticket-management/ticket-list/ticket-list').then(m => m.TicketList),
  },
  {
    path: 'create-ticket',
    loadComponent: () => import('./ticket-management/ticket-create/ticket-create').then(m => m.TicketCreate)
  }
];
