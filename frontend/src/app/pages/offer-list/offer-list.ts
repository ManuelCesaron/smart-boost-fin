import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { LoanResponseDto, LoanOfferDto } from '../../services/loan';

@Component({
  standalone: true,
  selector: 'app-offer-list',
  templateUrl: './offer-list.html',
  styleUrls: ['./offer-list.scss'],
  imports: [
    CommonModule,
    MatTableModule
  ]
})
export class OfferListComponent {

  /** risposta passata via router (history.state) */
  response = history.state as LoanResponseDto;

  /** offerte ordinate dal canone più basso al più alto */
  offers: LoanOfferDto[] = (this.response.offers ?? [])
    .slice()                                         // copia immutabile
    .sort((a, b) => a.monthlyInstallment - b.monthlyInstallment);

  /** l’indice dell’offerta “migliore” (rata minore) */
  bestIndex = 0;

  /** colonne da mostrare nella tabella */
  cols = ['bankName', 'annualRate', 'monthlyInstallment'];
}
