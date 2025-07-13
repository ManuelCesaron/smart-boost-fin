import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { LoanResponseDto } from '../../services/loan';

@Component({
  standalone: true,
  selector: 'app-offer-list',
  templateUrl: './offer-list.html',
  imports: [
    CommonModule,
    MatTableModule
  ]
})
export class OfferListComponent {
  response = history.state as LoanResponseDto;
  cols = ['bankName','annualRate','monthlyInstallment'];
}
