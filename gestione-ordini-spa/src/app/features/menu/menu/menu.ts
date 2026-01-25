// src/app/features/menu/menu/menu.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MenuService } from '../../../core/services/menu.service';
import { OrdersService } from '../../../core/services/order.service';
import { MenuItem } from '../../../core/models/menu.models';
import { ChangeDetectorRef, NgZone } from '@angular/core';
import { extractBackendWhy } from '../../../shared/utils/backend-error.util';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatFormFieldModule,
    MatInputModule
  ],
  templateUrl: './menu.html',
  styleUrls: ['./menu.scss']
})
export class MenuComponent implements OnInit {
  menu: MenuItem[] = [];
  selectedIds: number[] = [];
  address = '';
  message = '';

  constructor(private menuApi: MenuService, private ordersApi: OrdersService,
    private cdr: ChangeDetectorRef, private zone: NgZone) { }

  ngOnInit(): void {
    console.log('[MenuComponent] ngOnInit');

    this.menuApi.getMenu().subscribe({
      next: (items) => {
        console.log('[MenuComponent] received items:', items);
        console.log('[MenuComponent] items length:', items?.length);
          this.menu = items ?? [];
          this.cdr.detectChanges();
      },
      error: (err) => {
        console.log('[MenuComponent] getMenu error:', err);
          this.message = 'Failed';
          this.cdr.detectChanges();
      }
    });
  }


  addItem(id: number): void {
    this.selectedIds.push(id);
  }

  removeItem(id: number): void {
    const idx = this.selectedIds.indexOf(id);
    if (idx >= 0) this.selectedIds.splice(idx, 1);
  }

  qty(id: number): number {
    let count = 0;
    for (const x of this.selectedIds) {
      if (x === id) count++;
    }
    return count;
  }


  submitOrder(): void {
    this.message = '';
    if (!this.address.trim()) {
      this.message = 'Please enter a delivery address';
      return;
    }
    if (this.selectedIds.length === 0) {
      this.message = 'Select at least one item';
      return;
    }

    this.ordersApi
      .createOrder({
        indirizzo_Di_Consegna: this.address.trim(),
        contenuto: this.selectedIds
      })
      .subscribe({
        next: () => {
          this.message = 'Order created';
          this.cdr.detectChanges();
          this.selectedIds = [];
        },
        error: (e) => {
          this.message = extractBackendWhy(e, {defaultMessage : 'order creation failed'});
          this.cdr.detectChanges();
        }
      });
  }

  orderTotal(): number {
    if (this.selectedIds.length === 0) return 0;
    const priceMap = new Map(this.menu.map((item) => [item.id, item.prezzo]));
    return this.selectedIds.reduce((sum, id) => sum + (priceMap.get(id) ?? 0), 0);
  }
}
