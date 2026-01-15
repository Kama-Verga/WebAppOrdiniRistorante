import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../enviroment';
import { Observable } from 'rxjs';
import { CreateOrderRequest, OrdersQueryRequest, OrdersResponse } from '../models/order.models';
import { DebugUtil } from '../../shared/utils/debug.utils';
import { map } from 'rxjs';
@Injectable({ providedIn: 'root' })
export class OrdersService {
  private PREFIX = "OrderService";
  private base = environment.apiBaseUrl;
  constructor(private http: HttpClient) {}

  createOrder(payload: CreateOrderRequest): Observable<any> {
    // from doc: POST /Oridine/CreaOrdine :contentReference[oaicite:6]{index=6}
    return this.http.post(`${this.base}/Oridine/CreaOrdine`, payload);
  }

  listOrders(payload: OrdersQueryRequest): Observable<any[]> {
    // from doc: POST /Oridine/Visualizza Ordini :contentReference[oaicite:7]{index=7}
    var res = this.http
    .post<OrdersResponse>(`${this.base}/Oridine/Visualizza Ordini`, payload)
    .pipe(map((res) => res.ordini.ordine.result ?? []));
    DebugUtil.debug(this.PREFIX, res); 
    return res;
  }
}
