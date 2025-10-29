import apiService from "./api.service";

export interface LoginRequest {
    email: string;
    password: string;
}

export interface LoginResponse {
    token: string;
    expiresAt: string;
}

export interface SignupRequest {
    username: string;
    email: string;
    password: string;
}

export const TOKEN_KEY = "auth_token"
const EXPIRY_KEY = "auth_token_expires_at"

class AuthService {
    async login(request: LoginRequest): Promise<LoginResponse> {
        const response = await apiService.post<LoginResponse>("/auth/login", request);
        this.setToken(response);
        return response;
    }

    setToken(response: LoginResponse): void {
        localStorage.setItem(TOKEN_KEY, response.token);
        localStorage.setItem(EXPIRY_KEY, response.expiresAt);
    }

    getToken(): string | null {
        return localStorage.getItem(TOKEN_KEY);
    }

    getTokenExpiry(): Date | null {
        const expiry = localStorage.getItem(EXPIRY_KEY);
        return expiry ? new Date(expiry) : null;
    }

    isTokenValid(): boolean {
        const token = this.getToken();
        const expiry = this.getTokenExpiry();
    
        if (!token || !expiry) {
            return false;
        }

        return new Date() < expiry;
    }

    logout(): void {
        localStorage.removeItem(TOKEN_KEY);
        localStorage.removeItem(EXPIRY_KEY);
    }

    isAuthenticated(): boolean {
        return this.isTokenValid();
    }
}

export default new AuthService();