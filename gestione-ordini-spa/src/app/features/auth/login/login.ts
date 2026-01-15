import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { extractBackendWhy } from '../../../shared/utils/backend-error.util';
import { DebugUtil } from '../../../shared/utils/debug.utils';
import { ChangeDetectorRef, NgZone } from '@angular/core';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.html'
})
export class LoginComponent {
  form: FormGroup;
  error = '';
  PREFIX = 'LoginComponent'
  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router,
    private cdr: ChangeDetectorRef,
    private zone: NgZone
  ) {
    DebugUtil.debug(this.PREFIX, "started")
    this.form = this.fb.group({
      email: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  submit(): void {
    this.error = '';
    if (this.form.invalid) return;
    DebugUtil.debug(this.PREFIX, "form submitted")
    const email = this.form.get('email')?.value ?? '';
    const password = this.form.get('password')?.value ?? '';

    this.auth.login({ email, password }).subscribe({
      next: () => this.router.navigateByUrl('/menu'),
      error: (e) => {
        const msg = extractBackendWhy(e, { defaultMessage:'login failed.' });
        this.zone.run(() => {
          this.error = msg;
          this.cdr.detectChanges();
        })
        DebugUtil.debug(this.PREFIX, "error encountered", this.error);
      }
    });
  }
}
