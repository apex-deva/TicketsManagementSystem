import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { Ticket } from '../models/ticket.model';
const _BaseURL = 'https://localhost:44304';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection!: signalR.HubConnection;
  private baseUrl = _BaseURL; // Use environment variable

  public ticketCreated$ = new Subject<Ticket>();
  public ticketHandled$ = new Subject<string>();

  constructor() {
    this.initializeConnection();
  }

  private initializeConnection(): void {
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  private createConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${this.baseUrl}/ticketHub`, {
        skipNegotiation: true, // Important for proper handshake
        transport: signalR.HttpTransportType.WebSockets
      })
      .withAutomaticReconnect() // Add automatic reconnection
      .configureLogging(signalR.LogLevel.Debug) // For detailed logging
      .build();
  }
  private startConnection(): void {
    this.hubConnection
      .start()
      .then(() => console.log('SignalR connection established'))
      .catch(err => console.error('SignalR connection error:', err));
  }

  private registerOnServerEvents(): void {
    this.hubConnection.on('TicketCreated', (ticket: Ticket) => {
      this.ticketCreated$.next(ticket);
    });

    this.hubConnection.on('TicketHandled', (ticketId: string) => {
      this.ticketHandled$.next(ticketId);
    });

    this.hubConnection.onclose(() => {
      console.log('SignalR connection closed');
      setTimeout(() => this.startConnection(), 5000); // Reconnect after 5 seconds
    });
  }
}
