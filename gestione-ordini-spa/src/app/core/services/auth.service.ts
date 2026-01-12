import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../enviroment'; // inject environment variables
import { LoginRequest, RegisterRequest, TokenResponse } from '../models/auth.models'; // inject models
import { map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { TokenStorageService } from './token-storage.service'; // inject token storage service

@Injectable({ providedIn: 'root' })
export class AuthService {
  private base = environment.apiBaseUrl;

  constructor(private http: HttpClient, private tokenStore: TokenStorageService) {}

  register(payload: RegisterRequest): Observable<any> {
    // from doc: POST /Utente/Crea-Utente :contentReference[oaicite:3]{index=3}
    return this.http.post(`${this.base}/Utente/Crea-Utente`, payload);
  }

  login(payload: LoginRequest): Observable<string> {
    // from doc: POST /api/v1/Token/Create Token :contentReference[oaicite:4]{index=4}
    // If the space is real, this URL must be encoded. Angular will encode it if you put %20.
    const url = `${this.base}/api/v1/Token/Create%20Token`;

    return this.http.post<TokenResponse>(url, payload).pipe(
      map(res => res.token ?? res.accessToken ?? ''),
      tap(token => {
        if (!token) throw new Error('Token not found in response. Check backend response shape.');
        this.tokenStore.setToken(token);
      })
    );
  }

  logout(): void {
    this.tokenStore.clear();
  }

  isLoggedIn(): boolean {
    return !!this.tokenStore.getToken();
  }
}
