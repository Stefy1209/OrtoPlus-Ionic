import { TOKEN_KEY } from "./auth.service";

export interface ApiError {
  message: string;
  status: number;
  data?: any;
}

class ApiService {
  private baseURL: string;

  constructor() {
    this.baseURL = import.meta.env.VITE_API_URL || 'http://localhost:5189';
  }

  private getAuthHeader():Record<string, string> {
    const token = localStorage.getItem(TOKEN_KEY);
    return token ? { Authorization: `Bearer ${token}` } : {};
  }

  private async request<T>(
    endpoint: string,
    options: RequestInit = {}
  ): Promise<T> {

    const config: RequestInit = {
      ...options,
      headers: {
        'Content-Type': 'application/json',
        ...this.getAuthHeader(),
        ...options.headers,
      },
    };

    try {
      const response = await fetch(`${this.baseURL}${endpoint}`, config);

      if (!response.ok) {
        const errorData = await response.json().catch(() => ({}));
        const error: ApiError = {
          message: errorData.message || 'An error occurred',
          status: response.status,
          data: errorData,
        };
        throw error;
      }

      // Handle 204 No Content
      if (response.status === 204) {
        return {} as T;
      }

      return await response.json();
    } catch (error: any) {
      if (error.status) {
        throw error;
      }
      // Network error
      throw {
        message: 'Network error. Please check your connection.',
        status: 0,
      } as ApiError;
    }
  }

  async get<T>(endpoint: string): Promise<T> {
    return this.request<T>(endpoint, { method: 'GET' });
  }

  async post<T>(endpoint: string, body: any): Promise<T> {
    return this.request<T>(endpoint, {
      method: 'POST',
      body: JSON.stringify(body),
    });
  }

}

export default new ApiService();