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
import { SocialAuthService, GoogleLoginProvider, SocialUser } from '@abacritt/angularx-social-login';

@Injectable({
  providedIn: 'root'
})
export class AuthServicesService {
  currentDate!: Date;
  jwt!: string;
  rememberMe: boolean = false;
  externalGoogleInfo!: ExternalProviderDto;
  socialUser!: SocialUser;
  isLoggedin?: boolean;

  constructor(
    private cookieService: CookieService,
    private router: Router,
    private http: HttpClient,
    private socialAuthService: SocialAuthService

  ) {
    this.socialAuthService.authState.subscribe((user) => {
      this.socialUser = user;
      this.SignInWithGoogle();
    });
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
  async SignInWithGoogle() {

    this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);

    var googleUser: ExternalProviderDto = {
      provider: this.socialUser.provider,
      idToken: this.socialUser.idToken
    };
    var result = await this.http.post<AuthResponseDto>(`${environment.userHost}/googleLogin`, googleUser).toPromise();

    this.CookiesFactory(result?.token!);
    
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
