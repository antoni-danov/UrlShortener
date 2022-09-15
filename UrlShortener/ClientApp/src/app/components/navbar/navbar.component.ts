import { Component, OnInit } from '@angular/core';
import { AuthServicesService } from '../../services/auth/auth-services.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  isLoggedIn: boolean = true;

  constructor(
    private services: AuthServicesService
  ) { }

  ngOnInit(): void {
  }

  Logout() {
    this.isLoggedIn = false;
    return this.services.SignOut();
  }

}
