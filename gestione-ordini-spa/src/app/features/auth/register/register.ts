// src/app/features/auth/register/register.component.ts
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './register.html'
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


  // helper to parse backend errors on failed register
  private extractBackendWhy(err: unknown): string {
  // Fallback
  const defaultMsg = 'Registration failed. Please try again.';

  if (!(err instanceof HttpErrorResponse)) {
    // Sometimes people throw plain errors
    const anyErr = err as any;
    return anyErr?.message ?? defaultMsg;
  }

  // If backend sends a helpful body on 400, show it
  if (err.status === 400) {
    const body = err.error;

    // Case 1: body is plain text
    if (typeof body === 'string' && body.trim().length > 0) {
      return body;
    }

    // Case 2: body is JSON with common shapes
    if (body && typeof body === 'object') {
      // { message: "..." }
      if (typeof (body as any).message === 'string' && (body as any).message.trim().length > 0) {
        return (body as any).message;
      }

      // { error: "..." } or { title: "..." } (common in some APIs)
      for (const key of ['error', 'title', 'detail', 'reason']) {
        const v = (body as any)[key];
        if (typeof v === 'string' && v.trim().length > 0) return v;
      }

      // { errors: { field: ["msg1","msg2"], otherField: ["msg"] } } (common validation format)
      const errors = (body as any).errors;
      if (errors && typeof errors === 'object') {
        const messages: string[] = [];
        for (const field of Object.keys(errors)) {
          const fieldErrors = errors[field];
          if (Array.isArray(fieldErrors)) {
            for (const m of fieldErrors) {
              if (typeof m === 'string' && m.trim().length > 0) messages.push(m);
            }
          } else if (typeof fieldErrors === 'string' && fieldErrors.trim().length > 0) {
            messages.push(fieldErrors);
          }
        }
        if (messages.length > 0) return messages.join(' ');
      }

      // If object but unknown shape, try JSON stringify (not too noisy)
      try {
        const s = JSON.stringify(body);
        if (s && s !== '{}' && s !== 'null') return s;
      } catch {
        // ignore
      }
    }

    // If 400 but no usable body
    return 'Request not valid. Please check the form fields.';
  }

  // Non-400: keep a clean generic message (or show status text)
  if (typeof err.error === 'string' && err.error.trim().length > 0) return err.error;
  return err.message || defaultMsg;
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
        this.error = this.extractBackendWhy(e);
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
