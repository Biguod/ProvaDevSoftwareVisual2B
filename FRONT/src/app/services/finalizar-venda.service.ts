import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Venda } from '../models/Venda';

@Injectable({
  providedIn: 'root'
})
export class FinalizarVendaService {

    private baseUrl = "https://localhost:5001/api/venda";

    constructor(private http: HttpClient) {}

    create(item: Venda): Observable<Venda> {
        console.log(item)
        return this.http.post<Venda>(`${this.baseUrl}/create`, item);
    }
}
