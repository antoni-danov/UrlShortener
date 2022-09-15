import { Component, OnInit, AfterContentChecked } from '@angular/core';
import { AuthServicesService } from '../../services/auth/auth-services.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit, AfterContentChecked {

  isLoggedIn!: boolean;

  constructor(
    private services: AuthServicesService
  ) { }

  ngOnInit() {
    this.isLoggedIn = this.services.isAuthenticated();
  }
  ngAfterContentChecked() {
    this.ngOnInit();
  }

  Logout() {
    this.isLoggedIn = false;
    return this.services.SignOut();
  }

}
