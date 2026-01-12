// src/app/features/orders/orders/orders.component.ts
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersService } from '../../../core/services/order.service';
@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './orders.html'
})
export class OrdersComponent {
  orders: any[] = [];
  error = '';

  giornoInizio = new Date(Date.now() - 30 * 24 * 3600 * 1000).toISOString();
  giornoFine = new Date().toISOString();
  idUtente_Opsionale = 0;

  constructor(private ordersApi: OrdersService) {}

  load(): void {
    this.error = '';
    this.ordersApi
      .listOrders({
        giornoInizio: this.giornoInizio,
        giornoFine: this.giornoFine,
        idUtente_Opsionale: this.idUtente_Opsionale
      })
      .subscribe({
        next: (res) => (this.orders = res ?? []),
        error: () => (this.error = 'Failed to load orders')
      });
  }
}
