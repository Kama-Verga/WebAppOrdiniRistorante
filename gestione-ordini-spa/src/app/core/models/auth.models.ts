export interface RegisterRequest {
  email: string;
  nome: string;
  cognome: string;
  password: string;
  ruolo: number; 
}

export interface LoginRequest {
  email: string;
  password: string;
}

// don't remember wich one is used
export interface TokenResponse {
  token?: string;
  accessToken?: string;
}
