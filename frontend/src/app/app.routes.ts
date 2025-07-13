import { Routes } from '@angular/router';
import { CustomerFormComponent } from './pages/customer-form/customer-form';
import { LoanFormComponent }     from './pages/loan-form/loan-form';
import { OfferListComponent }    from './pages/offer-list/offer-list';

export const routes: Routes = [
  { path: '', redirectTo: 'customer', pathMatch: 'full' },
  { path: 'customer', component: CustomerFormComponent },
  { path: 'loan/:customerId', component: LoanFormComponent },
  { path: 'offers', component: OfferListComponent }
];
