import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthServicesService } from '../../services/auth/auth-services.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  userForm!: FormGroup;

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
      repeatPassword: new FormControl('', Validators.required)
    });
  }

  UserSignUp(userdata: any) {
    this.authServices.SignUpWithEmailAndPassword(userdata);
  }
  async SignInWithGoogle() {
    return await this.authServices.SignInWithPopUp();
  }
}
