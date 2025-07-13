import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { CustomerService, CustomerWithLoans }
  from '../../services/customer/customer';

@Component({
  standalone: true,
  selector: 'app-requests-list',
  templateUrl: './requests-list.html',
  styleUrls: ['./requests-list.scss'],
  imports: [CommonModule, MatTableModule]
})
export class RequestsListComponent {

  customers: CustomerWithLoans[] = [];
  displayed = ['customer', 'amount', 'term', 'rate', 'bank'];

  constructor(private custSvc: CustomerService) {
    this.custSvc.getAll().subscribe(customers => (this.customers = customers));
  }
}
