import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatFormFieldModule }  from '@angular/material/form-field';
import { MatInputModule }      from '@angular/material/input';
import { MatSelectModule }     from '@angular/material/select';
import { MatButtonModule }     from '@angular/material/button';
import { ActivatedRoute, Router } from '@angular/router';
import { LoanService, LoanRequestDto } from '../../services/loan';

@Component({
  standalone: true,
  selector: 'app-loan-form',
  templateUrl: './loan-form.html',
  styleUrls: ['./loan-form.scss'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule
  ]
})
export class LoanFormComponent {

  customerId!: number;          
  form!: FormGroup;             

  constructor(private fb: FormBuilder,
              private loanSvc: LoanService,
              private router: Router,
              private route: ActivatedRoute) {

    this.customerId = +this.route.snapshot.paramMap.get('customerId')!;

    this.form = this.fb.group({
      amount: [0, Validators.required],
      termMonths: [240, Validators.required],
      purpose: ['FirstHomePurchase', Validators.required],
      propertyValue: [],
      employmentType: ['Permanent', Validators.required],
      existingLoanMonthly: [0]
    });
  }

  submit(): void {
    if (this.form.invalid) return;

    const dto: LoanRequestDto = {
      customerId: this.customerId,
      ...this.form.value        // spread
    } as LoanRequestDto;

    this.loanSvc.applyLoan(dto).subscribe(resp => {
      this.router.navigate(['/offers'], { state: resp });
    });
  }
}
