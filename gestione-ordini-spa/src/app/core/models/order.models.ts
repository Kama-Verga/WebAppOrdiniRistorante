import { TaskJson } from "./task.models";

export interface CreateOrderRequest {
  indirizzo_Di_Consegna: string;
  contenuto: number[]; // ids/indices of dishes
}

export interface OrdersQueryRequest {
  giornoInizio: string; // ISO date string
  giornoFine: string;   // ISO date string
  idUtente_Opsionale: number; // 0 means “all” for admins per doc
}

export interface OrderDto {
  id: number;
  dataOrdine: string;
  totale: number;
}

export interface OrderItemInOrder {
  [key: string]: unknown;
}

export interface Order {
  numero_Ordine?: number;
  utenteId: number;
  data_creazione: string; // ISO string
  indirizzo_Di_Consegna: string;
  prezzo: number;
  prodottiInOrdine: OrderItemInOrder[];
}

export interface OrderByIdRequest {
  id_Ordine: number;
}

export interface OrderDetail {
  numeroOrdine: number;
  dataCreazione: string;
  indirizzoDiConsegna: string;
  prezzo: number;
  utenteId: number;
  prodottiId: number[];
}

export interface OrdersResponse {
  ordini: {
    ordine: TaskJson<Order[]>;
  };
}
