// src/app/core/services/menu.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../enviroment';
import { Observable, map, tap } from 'rxjs';
import { MenuItem } from '../models/menu.models';

@Injectable({ providedIn: 'root' })
export class MenuService {
  private base = environment.apiBaseUrl;
  constructor(private http: HttpClient) {}

  getMenu(): Observable<MenuItem[]> {
    return this.http.get<any>(`${this.base}/Menù`).pipe(
      tap((res) => {
        console.log('[MenuService] raw response:', res);
        console.log('[MenuService] top-level keys:', res ? Object.keys(res) : res);
        console.log('[MenuService] res.menu:', res?.menu);
        console.log('[MenuService] res.menu keys:', res?.menu ? Object.keys(res.menu) : res?.menu);
        console.log('[MenuService] res.menu.menu:', res?.menu?.menu);
        console.log('[MenuService] res.menu.menu keys:', res?.menu?.menu ? Object.keys(res.menu.menu) : res?.menu?.menu);
        console.log('[MenuService] res.menu.menu.result:', res?.menu?.menu?.result);
        console.log('[MenuService] isArray(res.menu.menu.result):', Array.isArray(res?.menu?.menu?.result));
      }),
      map((res) => {
        const direct = res?.menu?.menu?.result;

        // If the shape differs in practice, try a few candidates and log each one.
        const candidates: Array<{ name: string; value: any }> = [
          { name: 'res.menu.menu.result', value: res?.menu?.menu?.result },
          { name: 'res.menu.result', value: res?.menu?.result },
          { name: 'res.result', value: res?.result },
          { name: 'res.menu', value: res?.menu },
          { name: 'res', value: res }
        ];

        for (const c of candidates) {
          console.log(`[MenuService] candidate ${c.name}:`, c.value);
          console.log(`[MenuService] isArray(${c.name}):`, Array.isArray(c.value));
          if (Array.isArray(c.value)) {
            console.log(`[MenuService] USING ${c.name} (length=${c.value.length})`);
            return c.value as MenuItem[];
          }
        }

        // Sometimes APIs send JSON as a string. Try parsing if it looks like a string.
        if (typeof direct === 'string') {
          console.log('[MenuService] result is string, attempting JSON.parse');
          try {
            const parsed = JSON.parse(direct);
            console.log('[MenuService] parsed result:', parsed);
            if (Array.isArray(parsed)) return parsed as MenuItem[];
          } catch (e) {
            console.log('[MenuService] JSON.parse failed:', e);
          }
        }

        console.log('[MenuService] No array found, returning empty []');
        return [] as MenuItem[];
      }),
      tap((items) => {
        console.log('[MenuService] final items returned to component:', items);
        console.log('[MenuService] final items length:', items?.length);
      })
    );
  }
}
