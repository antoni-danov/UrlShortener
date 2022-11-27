import { Component, OnInit, AfterContentChecked } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthServicesService } from '../../services/auth/auth-services.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit, AfterContentChecked {

  isLoggedIn!: boolean;
  userGreetings!: string;

  constructor(
    private services: AuthServicesService,
    private cookies: CookieService,
    private router: Router
  ) { }

  ngOnInit() {
    this.isLoggedIn = this.services.IsAuthenticated();

    if (this.isLoggedIn == true) {
      this.userGreetings = this.cookies.get('email');
    }
  }
  ngAfterContentChecked() {
    this.ngOnInit();
  }

  Logout() {
    if (this.services.isExternalAuth == true) {
      this.services.GoogleSignOut();
      this.router.navigate(["/"]);
    }

    this.isLoggedIn = false;
    return this.services.SignOut();

  }

}
