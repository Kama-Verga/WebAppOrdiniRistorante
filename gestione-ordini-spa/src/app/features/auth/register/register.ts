// src/app/features/auth/register/register.component.ts
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { extractBackendWhy } from '../../../shared/utils/backend-error.util';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './register.html',
  styleUrls: ['./register.scss']
})
export class RegisterComponent {
  form: FormGroup;
  error = '';
  submitted = false;
  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router
  ) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      nome: ['', [Validators.required]],
      cognome: ['', [Validators.required]],
      password: ['', [Validators.required, strongPasswordValidator()]],
      ruolo: [0, [Validators.required]]
    });
  }

  // helper to check for validators errors
  shouldShowError(controlName: string) : boolean {
    const c = this.form.get(controlName);
    return !!(c && c.invalid && (c.touched || this.submitted));
  }
  // helper to get interpret errors from validators
  getErrorMessage(controlName: string): string {
    const c = this.form.get(controlName);
    if (!c || !c.errors) return '';

    if (c.errors['required']) return 'This field is required.';

    if (c.errors['email']) return 'Please enter a valid email address.';

    if (c.errors['minlength']) {
      const { requiredLength } = c.errors['minlength'];
      return `Minimum length is ${requiredLength} characters.`;
    }

    if (c.errors['strongPassword']) {
      const e = c.errors['strongPassword'];

      if (!e.hasMinLength) return 'Password must be at least 6 characters long.';
      if (!e.hasUpperCase) return 'Password must contain at least one uppercase letter.';
      if (!e.hasNumber) return 'Password must contain at least one number.';
      if (!e.hasSpecialChar) return 'Password must contain at least one special character.';

      return 'Password does not meet security requirements.';
    }

    return 'Invalid value.';
  }
  
 submit(): void {
    this.submitted = true;
    this.error = '';

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.auth.register(this.form.value as any).subscribe({
      next: () => this.router.navigateByUrl('/login'),
      error: (e) => {
        this.error = extractBackendWhy(e,{
          defaultMessage: 'registration failed.'
        });
      }
    });
  }
}


// custom validator for pasword requirements (same as backend)
import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

export function strongPasswordValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value as string;
    if (!value) return null;

    const hasLowerCase = /[a-z]/.test(value);
    const hasUpperCase = /[A-Z]/.test(value);
    const hasNumber = /[0-9]/.test(value);
    const hasSpecialChar = /[!@#$%^&*(),.?":{}|<>_\-+=~`[\]\\;/]/.test(value);
    const hasMinLength = value.length >= 6;

    const valid =
      hasUpperCase &&
      hasNumber &&
      hasSpecialChar &&
      hasLowerCase &&
      hasMinLength;

    return valid
      ? null
      : {
          strongPassword: {
            hasUpperCase,
            hasNumber,
            hasSpecialChar,
            hasMinLength
          }
        };
  };
}
