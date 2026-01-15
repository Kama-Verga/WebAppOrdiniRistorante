// src/app/core/services/menu.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../enviroment';
import { Observable, map, tap } from 'rxjs';
import { MenuItem, MenuResponse } from '../models/menu.models';

@Injectable({ providedIn: 'root' })
export class MenuService {
  private base = environment.apiBaseUrl;
  constructor(private http: HttpClient) {}

  getMenu(): Observable<MenuItem[]> {
    return this.http.get<MenuResponse>(`${this.base}/Menù`).pipe(
      map((res) => {
        const items = res?.menu?.menu?.result;
        return Array.isArray(items) ? items : [];
      })
    );
  }
}