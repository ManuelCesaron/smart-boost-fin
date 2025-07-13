import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface LoanApp {
  id: number;
  amount: number;
  termMonths: number;
  monthlyInstallment: number;
  status: string;
  bank: { name: string };
}

export interface CustomerWithLoans {
  id: number;
  firstName: string;
  lastName: string;
  annualGrossSalary: number;
  loanApplications: LoanApp[];
}

@Injectable({ providedIn: 'root' })
export class CustomerService {
  constructor(private http: HttpClient) {}

  getAll(): Observable<CustomerWithLoans[]> {
    return this.http.get<CustomerWithLoans[]>('/api/customers');
  }
}
