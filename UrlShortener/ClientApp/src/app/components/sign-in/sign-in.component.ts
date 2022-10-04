import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthServicesService } from '../../services/auth/auth-services.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {

  form!: FormGroup;

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

  async SignInEmailAndPassword(userdata: any) {
    return await this.authServices.SignInWithEmailAndPassword(userdata);

  }
}
