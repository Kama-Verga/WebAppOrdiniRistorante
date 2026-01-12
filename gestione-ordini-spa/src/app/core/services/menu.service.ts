import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../enviroment'; // inject environment variables
import { Observable } from 'rxjs';
import { MenuItem } from '../models/menu.models'; // inject menu models

@Injectable({ providedIn: 'root' })
export class MenuService {
  private base = environment.apiBaseUrl;
  constructor(private http: HttpClient) {}

  getMenu(): Observable<MenuItem[]> {
    // from doc: GET /Menù :contentReference[oaicite:5]{index=5}
    return this.http.get<MenuItem[]>(`${this.base}/Menù`);
  }
}
