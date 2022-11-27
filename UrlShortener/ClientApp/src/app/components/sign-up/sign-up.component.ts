import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterUserDto } from '../../interfaces/user/RegisterUserDto';
import { AuthServicesService } from '../../services/auth/auth-services.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  userForm!: FormGroup;
  errors!: string[];

  constructor(
    private authServices: AuthServicesService,
    private router: Router
  ) { }

  ngOnInit() {
    if (this.authServices.IsAuthenticated()) {
      this.router.navigate(['/']);
    }

    this.userForm = new FormGroup({
      email: new FormControl('', Validators.compose([
        Validators.required,
        Validators.pattern('^[A-za-z0-9._%+-]+@[a-z]{3,6}\.[a-z]{2,4}$')
      ])),
      password: new FormControl('', Validators.required),
      confirmPassword: new FormControl('', Validators.required)
    });
  }

  UserSignUp(userdata: RegisterUserDto) {
    this.authServices.CreateUser(userdata)
      .then()
      .catch((err: HttpErrorResponse) => {
        this.errors = err.error;
        setTimeout(() => this.errors = [], 5000);
      });
  }

  async SignInWithGoogle() {

  }
}
