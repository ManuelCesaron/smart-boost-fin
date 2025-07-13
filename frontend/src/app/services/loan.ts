import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

/* ---------- 1. DTO che useremo per le chiamate ---------- */

export interface CustomerCreateDto {
  firstName: string;
  lastName:  string;
  annualGrossSalary: number;
}

export interface LoanRequestDto {
  customerId: number;
  amount: number;
  termMonths: number;
  purpose: 'FirstHomePurchase' | 'PersonalLoan';
  propertyValue?: number;
  employmentType: 'Permanent' | 'FixedTerm';
  existingLoanMonthly: number;
}

export interface LoanOfferDto {
  bankName: string;
  annualRate: number;
  termMonths: number;
  monthlyInstallment: number;
}

export interface LoanResponseDto {
  status: 'Approved' | 'Rejected';
  offers: LoanOfferDto[];
}

/* ---------- 2. Servizio vero e proprio ---------- */

@Injectable({ providedIn: 'root' })
export class LoanService {

  constructor(private http: HttpClient) {}

  /** POST /api/customers — crea il cliente e restituisce l’oggetto con id */
  createCustomer(dto: CustomerCreateDto): Observable<any> {
    return this.http.post('/api/customers', dto);
  }

  /** POST /api/loanapplications — invia la richiesta di mutuo/prestito */
  applyLoan(dto: LoanRequestDto): Observable<LoanResponseDto> {
    return this.http.post<LoanResponseDto>('/api/loanapplications', dto);
  }
}
