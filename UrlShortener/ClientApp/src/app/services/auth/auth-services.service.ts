import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import jwt_decode from "jwt-decode";
import { CookieService } from 'ngx-cookie-service';
import { environment } from '../../../environments/environment.prod';
import { AuthResponseDto } from '../../interfaces/response/AuthResponseDto';
import { LoginUserDto } from '../../interfaces/user/LoginUserDto';
import { RegisterUserDto } from '../../interfaces/user/RegisterUserDto';
import { TokenData } from '../../models/TokenData';
import { ExternalProviderDto } from '../../models/ExternalProviderDto';

@Injectable({
  providedIn: 'root'
})
export class AuthServicesService {
  currentDate!: Date;
  jwt!: string;
  rememberMe: boolean = false;
  externalGoogleInfo!: ExternalProviderDto;

  constructor(
    //private afAuth: AngularFireAuth,
    private cookieService: CookieService,
    private router: Router,
    private http: HttpClient
  ) {
  }

  async SignInWithGoogle(data: string) {

    await this.http.post<AuthResponseDto>(`${environment.userHost}/GoogleLogin`, data).toPromise();

    return this.router.navigate(['/']);
  }

  async CreateUser(userdata: RegisterUserDto) {
    var registerUser = await this.http.post<AuthResponseDto>(`${environment.userHost}/register`, userdata).toPromise();

    this.CookiesFactory(registerUser?.token!);
    this.router.navigateByUrl('/');
  }
  async LoginUser(userdata: LoginUserDto) {
    var userLogedIn = await this.http.post<AuthResponseDto>(`${environment.userHost}/login`, userdata).toPromise();

    await this.CookiesFactory(userLogedIn?.token!);
    this.router.navigateByUrl('/');
  }
  async GoogleLogin() {
    //var googleUser: ExternalProviderDto = {
    //  provider: data.provider,
    //  idToken: data.idToken
    //};

    //this.CookiesFactory(data.idToken);
    

    //this.router.navigateByUrl('/');
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
  async CookiesFactory(jwt: string) {
    this.currentDate = new Date();
    this.currentDate.setHours(this.currentDate.getHours() + 12);

    await this.cookieService.set('JWT', jwt, { expires: this.currentDate, secure: true });

    await this.RetrieveTokenInformation(this.cookieService.get('JWT'));
  }
  async RetrieveTokenInformation(token: string) {
    var decode: TokenData = await jwt_decode(token);

    if (decode != null) {

      await this.cookieService.set('uid', decode.uid, { expires: this.currentDate, secure: true });
      await this.cookieService.set('email', decode.email, { expires: this.currentDate, secure: true });
    }
  }
}
