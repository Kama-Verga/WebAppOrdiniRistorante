import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../enviroment';
import { Observable } from 'rxjs';
import { CreateOrderRequest, OrdersQueryRequest } from '../models/order.models';

@Injectable({ providedIn: 'root' })
export class OrdersService {
  private base = environment.apiBaseUrl;
  constructor(private http: HttpClient) {}

  createOrder(payload: CreateOrderRequest): Observable<any> {
    // from doc: POST /Oridine/CreaOrdine :contentReference[oaicite:6]{index=6}
    return this.http.post(`${this.base}/Oridine/CreaOrdine`, payload);
  }

  listOrders(payload: OrdersQueryRequest): Observable<any[]> {
    // from doc: POST /Oridine/Visualizza Ordini :contentReference[oaicite:7]{index=7}
    return this.http.post<any[]>(`${this.base}/Oridine/Visualizza Ordini`, payload);
  }
}
