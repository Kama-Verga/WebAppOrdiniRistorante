import { Component } from '@angular/core';
import { RouterLink, RouterOutlet, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterOutlet],
  templateUrl: './app.html'
})
export class AppComponent {
  constructor(public auth: AuthService,private router: Router) {}
  logout(): void {
    this.auth.logout();
    this.router.navigateByUrl("/login")
  }
}
