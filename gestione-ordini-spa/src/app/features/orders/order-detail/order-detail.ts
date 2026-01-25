import { ChangeDetectorRef, Component, NgZone, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { OrdersService } from '../../../core/services/order.service';
import { OrderDetail } from '../../../core/models/order.models';
import { extractBackendWhy } from '../../../shared/utils/backend-error.util';
import { MenuService } from '../../../core/services/menu.service';
import { MenuItem } from '../../../core/models/menu.models';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatDividerModule } from '@angular/material/divider';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableModule } from '@angular/material/table';
import { DebugUtil } from '../../../shared/utils/debug.utils';

interface OrderLineItem {
  id: number;
  name: string;
  price: number;
  quantity: number;
  subtotal: number;
}

@Component({
  selector: 'app-order-detail',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatCardModule,
    MatButtonModule,
    MatChipsModule,
    MatDividerModule,
    MatProgressSpinnerModule,
    MatTableModule
  ],
  templateUrl: './order-detail.html',
  styleUrls: ['./order-detail.scss']
})
export class OrderDetailComponent implements OnInit {
  PREFIX = "OrderDetailComponent";
  order: OrderDetail | null = null;
  orderId: number | null = null;
  loading = true;
  error = '';
  menuError = '';
  menu: MenuItem[] = [];
  lineItems: OrderLineItem[] = [];
  computedTotal = 0;
  menuLoading = false;
  displayedColumns: string[] = ['id', 'name', 'quantity', 'price', 'subtotal'];

  constructor(
    private cdr: ChangeDetectorRef,
    private ordersApi: OrdersService,
    private route: ActivatedRoute,
    private menuApi: MenuService,
    private zone: NgZone
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const rawId = params.get('id');
      const parsedId = rawId ? Number(rawId) : NaN;
      DebugUtil.debug(this.PREFIX,"parsedId:" + parsedId)
      if (!rawId || Number.isNaN(parsedId) || parsedId <= 0) {
        this.zone.run(() => {
          this.error = 'Order id is invalid.';
          this.loading = false;
          this.orderId = null;
          this.order = null;
          this.cdr.detectChanges();
        });
        return;
      }

      this.orderId = parsedId;
      this.fetchOrder(parsedId);
      DebugUtil.debug(this.PREFIX, "fetched: " + this.order);
    });
  }

  private fetchOrder(orderId: number): void {
    this.zone.run(() => {
      this.loading = true;
      this.error = '';
      this.menuError = '';
      this.order = null;
      this.lineItems = [];
      this.computedTotal = 0;
      this.menuLoading = false;
      this.cdr.detectChanges();
    });

    this.ordersApi.getOrderById({ id_Ordine: orderId }).subscribe({
      next: (res) => {
        this.zone.run(() => {
          if (!res) {
            this.error = 'Order not found.';
            this.order = null;
          } else {
            this.order = res;
            DebugUtil.debug(this.PREFIX, this.order);
          }
          this.loading = false;
          this.cdr.detectChanges();
        });
        if (res && (res.prodottiId?.length ?? 0) > 0) {
          this.loadMenuForOrder(res);
        } else {
          this.zone.run(() => {
            this.lineItems = [];
            this.computedTotal = 0;
            this.menuLoading = false;
            this.cdr.detectChanges();
          });
        }
      },
      error: (e) => {
        this.zone.run(() => {
          this.error = extractBackendWhy(e, { defaultMessage: 'Failed to load order details' });
          this.loading = false;
          this.cdr.detectChanges();
        });
      }
    });
  }

  private loadMenuForOrder(order: OrderDetail): void {
    this.zone.run(() => {
      this.menuLoading = true;
      this.cdr.detectChanges();
    });

    this.menuApi.getMenu().subscribe({
      next: (items) => {
        this.zone.run(() => {
          this.menu = items ?? [];
          this.buildLineItems(order);
          this.menuLoading = false;
          this.cdr.detectChanges();
        });
      },
      error: (e) => {
        this.zone.run(() => {
          this.menuError = extractBackendWhy(e, { defaultMessage: 'Failed to load menu for order details' });
          this.menu = [];
          this.buildLineItems(order);
          this.menuLoading = false;
          this.cdr.detectChanges();
        });
      }
    });
  }

  private buildLineItems(order: OrderDetail): void {
    const counts = new Map<number, number>();
    for (const id of order.prodottiId ?? []) {
      counts.set(id, (counts.get(id) ?? 0) + 1);
    }

    const menuById = new Map(this.menu.map((item) => [item.id, item]));
    const items: OrderLineItem[] = [];

    counts.forEach((quantity, id) => {
      const menuItem = menuById.get(id);
      const price = menuItem?.prezzo ?? 0;
      items.push({
        id,
        name: menuItem?.nome ?? 'Unknown item',
        price,
        quantity,
        subtotal: price * quantity
      });
    });

    items.sort((a, b) => a.id - b.id);
    this.lineItems = items;
    this.computedTotal = items.reduce((sum, item) => sum + item.subtotal, 0);
  }
}
