// src/app/features/menu/menu/menu.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MenuService } from '../../../core/services/menu.service';
import { OrdersService } from '../../../core/services/order.service';
import { MenuItem } from '../../../core/models/menu.models';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './menu.html'
})
export class MenuComponent implements OnInit {
  menu: MenuItem[] = [];
  selectedIds: number[] = [];
  address = '';
  message = '';

  constructor(private menuApi: MenuService, private ordersApi: OrdersService) {}

  ngOnInit(): void {
    this.menuApi.getMenu().subscribe({
      next: (items) => (this.menu = items ?? []),
      error: () => (this.message = 'Failed to load menu')
    });
  }

  toggle(id: number): void {
    if (!id) return;
    if (this.selectedIds.includes(id)) {
      this.selectedIds = this.selectedIds.filter((x) => x !== id);
    } else {
      this.selectedIds = [...this.selectedIds, id];
    }
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
          this.selectedIds = [];
        },
        error: () => (this.message = 'Order failed')
      });
  }
}
