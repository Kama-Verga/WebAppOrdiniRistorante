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

  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router
  ) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      nome: ['', [Validators.required]],
      cognome: ['', [Validators.required]],
      password: ['', [Validators.required]],
      ruolo: [0, [Validators.required]]
    });
  }

  submit(): void {
    this.error = '';
    if (this.form.invalid) return;

    this.auth.register(this.form.value as any).subscribe({
      next: () => this.router.navigateByUrl('/login'),
      error: (e) => (this.error = e?.message ?? 'Registration failed')
    });
  }
}
