export interface CreateOrderRequest {
  indirizzo_Di_Consegna: string;
  contenuto: number[]; // ids/indices of dishes
}

export interface OrdersQueryRequest {
  giornoInizio: string; // ISO date string
  giornoFine: string;   // ISO date string
  idUtente_Opsionale: number; // 0 means “all” for admins per doc
}
