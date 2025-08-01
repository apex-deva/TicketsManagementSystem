import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { PagedResult, Ticket } from '../models/ticket.model';
import { TicketService } from '../ticket.service';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-ticket-list',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  providers:[TicketService],
  templateUrl: './ticket-list.html',
  styleUrl: './ticket-list.scss'
})
export class TicketList implements OnInit, OnDestroy {
  tickets: Ticket[] = [];
  currentPage: number = 1;
  pageSize: number = 5;
  totalPages: number = 0;
  totalCount: number = 0;
  loading: boolean = false;

  private subscriptions: Subscription[] = [];

  constructor(private ticketService: TicketService, private cdr: ChangeDetectorRef , private router: Router) {}

  ngOnInit() {
    this.loadTickets();
  }

  ngOnDestroy() {
    this.subscriptions.forEach(sub => {
      if (sub) {
        sub.unsubscribe();
      }
    });
    this.subscriptions = [];
  }

  loadTickets() {
    this.loading = true;
    const sub = this.ticketService.getTickets(this.currentPage, this.pageSize).subscribe({
      next: (result: PagedResult<Ticket>) => {
        this.tickets = result.items;
        this.totalPages = result.totalPages;
        this.totalCount = result.totalCount;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error('Error loading tickets:', error);
        this.loading = false;
      }
    });
    this.subscriptions.push(sub);
  }

  handleTicket(ticketId: string) {
    const sub = this.ticketService.handleTicket(ticketId).subscribe({
      next: () => {
       
        this.loadTickets();
      },
      error: (error) => {
        console.error('Error handling ticket:', error);
      }
    });
    this.subscriptions.push(sub);
  }

  getColorClass(colorCode: string): string {
    switch (colorCode.toLowerCase()) {
      case 'yellow': return 'ticket-yellow';
      case 'green': return 'ticket-green';
      case 'blue': return 'ticket-blue';
      case 'red': return 'ticket-red';
      default: return 'ticket-default';
    }
  }

  onPageChange(page: number) {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.loadTickets();
    }
  }

   goToCreateTicket(): void {
    this.router.navigate(['../create-ticket']); // Adjust the route as necessary
  }
}