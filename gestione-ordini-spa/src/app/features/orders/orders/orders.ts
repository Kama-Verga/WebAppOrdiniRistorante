// src/app/features/orders/orders/orders.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersService } from '../../../core/services/order.service';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { extractBackendWhy } from '../../../shared/utils/backend-error.util';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './orders.html'
})
export class OrdersComponent implements OnInit {
  orders: any[] = [];
  error = '';
  form: FormGroup;

  // Keep the same names you had commented out, but actually define them.
  // If you really need them in the payload, keep them here.
  idUtente_Opsionale = 0;

  constructor(private ordersApi: OrdersService, private fb: FormBuilder) {
    this.form = this.fb.group({
      // HTML date inputs expect 'YYYY-MM-DD'
      giornoInizio: [''],
      giornoFine: ['']
    });
  }

  ngOnInit(): void {
    const today = new Date();
    const yesterday = new Date();
    yesterday.setDate(today.getDate() - 1);

    this.form.patchValue({
      giornoInizio: this.toInputDate(yesterday),
      giornoFine: this.toInputDate(today)
    });

    // Optional: auto-load on open
    this.load();
  }

  load(): void {
    this.error = '';

    const giornoInizioStr = this.form.get('giornoInizio')?.value as string;
    const giornoFineStr = this.form.get('giornoFine')?.value as string;

    if (!giornoInizioStr || !giornoFineStr) {
      this.error = 'Please select both start and end dates.';
      return;
    }

    this.ordersApi
      .listOrders({
        // Convert YYYY-MM-DD to ISO (server-friendly)
        giornoInizio: new Date(`${giornoInizioStr}T00:00:00`).toISOString(),
        giornoFine: new Date(`${giornoFineStr}T23:59:59`).toISOString(),
        idUtente_Opsionale: this.idUtente_Opsionale
      })
      .subscribe({
        next: (res) => {
          this.orders = res ?? [];
          console.log(res);
        },
        error: (e) => {
          this.error = extractBackendWhy(e, { defaultMessage: 'Failed to load orders' });
        }
      });
  }

  private toInputDate(date: Date): string {
    // YYYY-MM-DD for <input type="date">
    return date.toISOString().split('T')[0];
  }
}
