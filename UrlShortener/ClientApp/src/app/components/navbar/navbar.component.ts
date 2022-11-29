import { Component, OnInit, AfterContentChecked } from '@angular/core';
<<<<<<< HEAD
import { Router } from '@angular/router';
=======
>>>>>>> f791317d5baabd14d93217b495ab8e033e4ed2e1
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
<<<<<<< HEAD
    private cookies: CookieService,
    private router: Router
=======
    private cookies: CookieService
>>>>>>> f791317d5baabd14d93217b495ab8e033e4ed2e1
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
    this.isLoggedIn = false;
    return this.services.SignOut();
  }

}
