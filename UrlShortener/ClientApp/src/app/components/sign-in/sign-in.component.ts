import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginUserDto } from '../../interfaces/user/LoginUserDto';
import { AuthServicesService } from '../../services/auth/auth-services.service';


@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {

  form!: FormGroup;
  errors: string[] = [];
  showError: any;

  constructor(
    private authServices: AuthServicesService,
    private router: Router
  ) { }

  ngOnInit() {
    if (this.authServices.IsAuthenticated()) {
      this.router.navigate(['/']);
    }

    this.form = new FormGroup({
      email: new FormControl('', Validators.compose([
        Validators.required,
        Validators.pattern('^[A-za-z0-9._%+-]+@[a-z]{3,6}\.[a-z]{2,4}$')
      ])),
      password: new FormControl('', Validators.required),
      checkbox: new FormControl('')
    });


  }

  SignInEmailAndPassword(userdata: LoginUserDto) {
    this.authServices.LoginUser(userdata)
      .then()
      .catch((err: HttpErrorResponse) => {

        this.errors = err.error;
        setTimeout(() => this.errors = [], 5000);
      });

  }
  SignInWithGoogle(event: any) {
    var provider = event.target.firstChild.data;

    this.authServices.SignInWithGoogle(provider)
      .then()
      .catch(err => {
        console.log(err);
      });
  }
}


