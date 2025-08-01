import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { CreateTicketRequest, PagedResult, Ticket } from './models/ticket.model';
import { Observable } from 'rxjs';

const _BaseURL = 'https://localhost:44304';

@Injectable({
  providedIn: 'root'
})
export class TicketService {
    private apiUrl = `${_BaseURL}/api/tickets`;
constructor(private http: HttpClient) {}

  createTicket(ticket: CreateTicketRequest): Observable<Ticket> {
    return this.http.post<Ticket>(this.apiUrl, ticket);
  }

  getTickets(page: number = 1, pageSize: number = 5): Observable<PagedResult<Ticket>> {
    return this.http.get<PagedResult<Ticket>>(`${this.apiUrl}?page=${page}&pageSize=${pageSize}`);
  }

  handleTicket(ticketId: string): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${ticketId}/handle`, {});
  }
} 
