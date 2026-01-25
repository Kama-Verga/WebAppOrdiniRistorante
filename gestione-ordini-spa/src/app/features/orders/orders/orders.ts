// src/app/features/orders/orders/orders.component.ts
import { ChangeDetectorRef, Component, DebugElement, NgZone, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersService } from '../../../core/services/order.service';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { extractBackendWhy } from '../../../shared/utils/backend-error.util';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { Router } from '@angular/router';
import { Order } from '../../../core/models/order.models';
import { DebugUtil } from '../../../shared/utils/debug.utils';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
    MatTableModule,
    MatChipsModule
  ],
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: 'it-IT' },
    { provide: MAT_DATE_FORMATS, useValue: { display: { dateInput: 'dd/MM/yyyy' }, parse: { dateInput: 'dd/MM/yyyy' } } }
  ],
  templateUrl: './orders.html',
  styleUrls: ['./orders.scss']
})
export class OrdersComponent implements OnInit {
  PREFIX = "OrdersComponent";
  orders: Order[] = [];
  error = '';
  form: FormGroup;
  displayedColumns: string[] = ['id', 'date', 'address', 'user', 'total', 'items', 'detail'];

  // Keep the same names you had commented out, but actually define them.
  // If you really need them in the payload, keep them here.
  idUtente_Opsionale = 0;

  constructor(private cdr : ChangeDetectorRef ,private ordersApi: OrdersService, private fb: FormBuilder, private router: Router) {
    this.form = this.fb.group({
      giornoInizio: [null as Date | null],
      giornoFine: [null as Date | null]
    });
  }

  ngOnInit(): void {
    const today = new Date();
    const yesterday = new Date();
    yesterday.setDate(today.getDate() - 1);

    this.form.patchValue({
      giornoInizio: yesterday,
      giornoFine: today
    });

    // Optional: auto-load on open
    this.load();
  }

  load(): void {
    this.error = '';

    const giornoInizioValue = this.form.get('giornoInizio')?.value;
    const giornoFineValue = this.form.get('giornoFine')?.value;
    const giornoInizioDate = this.coerceDate(giornoInizioValue);
    const giornoFineDate = this.coerceDate(giornoFineValue);

    if (!giornoInizioDate || !giornoFineDate) {
      this.error = 'Please select both start and end dates.';
      return;
    }

    const start = new Date(giornoInizioDate);
    start.setHours(0, 0, 0, 0);
    const end = new Date(giornoFineDate);
    end.setHours(23, 59, 59, 999);

    this.ordersApi
      .listOrders({
        // Convert YYYY-MM-DD to ISO (server-friendly)
        giornoInizio: start.toISOString(),
        giornoFine: end.toISOString(),
        idUtente_Opsionale: this.idUtente_Opsionale
      })
      .subscribe({
        next: (res) => {
          this.orders = res ?? [];
          console.log(res);
          this.cdr.detectChanges();
        },
        error: (e) => {
          this.error = extractBackendWhy(e, { defaultMessage: 'Failed to load orders' });
          this.cdr.detectChanges();
        }
      });
  }

  private coerceDate(value: unknown): Date | null {
    if (value instanceof Date && !Number.isNaN(value.getTime())) {
      return value;
    }
    if (typeof value === 'string' && value) {
      const parsed = new Date(value);
      return Number.isNaN(parsed.getTime()) ? null : parsed;
    }
    return null;
  }

  openDetails(order: Order): void {
    DebugUtil.debug(this.PREFIX, order.numero_Ordine)
    const orderId = order.numero_Ordine;
    if (!orderId) {
      this.error = 'Order id is missing for the selected entry.';
      return;
    }
    this.router.navigate(['/orders', orderId]);
  }
}
