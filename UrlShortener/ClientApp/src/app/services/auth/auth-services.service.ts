import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
//import { AngularFireAuth } from '@angular/fire/compat/auth';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { environment } from '../../../environments/environment.prod';
import { AuthResponseDto } from '../../interfaces/response/AuthResponseDto';
import { RegistrationResponseDto } from '../../interfaces/response/RegistrationResponseDto';
import { LoginUserDto } from '../../interfaces/user/LoginUserDto';
import { RegisterUserDto } from '../../interfaces/user/RegisterUserDto';

@Injectable({
  providedIn: 'root'
})
export class AuthServicesService {
  currentDate!: Date;
  uid: any;
  jwt!: string;
  email!: string;
  user: any;
  rememberMe: boolean = false;

  constructor(
    //private afAuth: AngularFireAuth,
    private cookieService: CookieService,
    private router: Router,
    private http: HttpClient
  ) { }

  //async SignInWithPopUp() {
  //  var provider = new firebase.default.auth.GoogleAuthProvider();

  //  await firebase.default.auth().signInWithPopup(provider)
  //    .then((result) => {
  //      this.jwt = result?.credential?.signInMethod!;
  //      this.uid = result.user?.uid;
  //      this.email = result?.user?.email!;
  //    }).catch(function (error) {
  //      var errorCode = error.code;
  //      var errorMessage = error.message;
  //      var email = error.email;
  //      var credential = error.credential;
  //    });

  //  this.CreateUser(this.uid);

  //  this.CookiesFactory(this.cookie);

  //  this.router.navigateByUrl('/');
  //}
 
  async CreateUser(userdata: RegisterUserDto) {
    var registerUser = await this.http.post<AuthResponseDto>(`${environment.userHost}/register`, userdata).toPromise();

    this.CookiesFactory(registerUser?.token!, registerUser?.email!, registerUser?.uid!);
    this.router.navigateByUrl('/');
  }
  async LoginUser(userdata: LoginUserDto) {
    var userLogedIn = await this.http.post<AuthResponseDto>(`${environment.userHost}/login`, userdata).toPromise();

    this.CookiesFactory(userLogedIn?.token!, userLogedIn?.email!, userLogedIn?.uid!);
    this.router.navigateByUrl('/');
  }
  SignOut() {
    this.cookieService.deleteAll();
    this.http.get(`${environment.userHost}/logout`).toPromise();

    return this.router.navigate(['/']);
  }


  IsAuthenticated() {

    const checkUser = this.cookieService.get('JWT');

    if (checkUser) {
      return true;
    }
    return false;
  }
  CookiesFactory(jwt: string, email: string, uid: string) {
    this.currentDate = new Date();
    this.currentDate.setHours(this.currentDate.getHours() + 12);

    this.cookieService.set('JWT', jwt, { expires: this.currentDate, secure: true });
    this.cookieService.set('Email', email, { expires: this.currentDate, secure: true });
    this.cookieService.set('Uid', uid, { expires: this.currentDate, secure: true });
  }
}
