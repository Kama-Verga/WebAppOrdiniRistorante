export interface RegisterRequest {
  email: string;
  nome: string;
  cognome: string;
  password: string;
  ruolo: number; // from your doc: 0/...
}

export interface LoginRequest {
  email: string;
  password: string;
}

// Token response is not specified in your doc; adjust to match your backend.
// Common options: { token: string } or { accessToken: string }
export interface TokenResponse {
  token?: string;
  accessToken?: string;
}
