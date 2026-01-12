import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.html'
})
export class LoginComponent {
  form: FormGroup;
  error = '';

  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router
  ) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  submit(): void {
    this.error = '';
    if (this.form.invalid) return;

    const email = this.form.get('email')?.value ?? '';
    const password = this.form.get('password')?.value ?? '';

    this.auth.login({ email, password }).subscribe({
      next: () => this.router.navigateByUrl('/menu'),
      error: (e) => (this.error = e?.message ?? 'Login failed')
    });
  }
}
