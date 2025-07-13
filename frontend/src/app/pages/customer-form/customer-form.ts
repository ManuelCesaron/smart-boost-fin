import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { MatFormFieldModule }  from '@angular/material/form-field';
import { MatInputModule }      from '@angular/material/input';
import { MatButtonModule }     from '@angular/material/button';
import { LoanService } from '../../services/loan';

@Component({
  standalone: true,
  selector: 'app-customer-form',
  templateUrl: './customer-form.html',
  imports: [                
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ]
})
export class CustomerFormComponent {

  form!: FormGroup;                     // 1. dichiarata ma non inizializzata

  constructor(private fb: FormBuilder,
              private loanSvc: LoanService,
              private router: Router) {

    // 2. inizializzata qui, dove fb è già disponibile
    this.form = this.fb.group({
      firstName: ['', Validators.required],
      lastName:  ['', Validators.required],
      annualGrossSalary: [0, [Validators.required, Validators.min(1)]]
    });
  }

  submit(): void {
    if (this.form.invalid) return;

    this.loanSvc.createCustomer(this.form.value!).subscribe(cust => {
      this.router.navigate(['/loan', cust.id]);
    });
  }
}
