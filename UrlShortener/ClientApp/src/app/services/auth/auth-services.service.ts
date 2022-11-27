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
import { SocialAuthService, SocialUser } from "@abacritt/angularx-social-login";
import { GoogleLoginProvider } from "@abacritt/angularx-social-login";
import { Subject } from 'rxjs';
import { ExternalProviderDto } from '../../models/ExternalProviderDto';

@Injectable({
  providedIn: 'root'
})
export class AuthServicesService {
  currentDate!: Date;
  jwt!: string;
  rememberMe: boolean = false;
  extAuthChangeSub = new Subject<SocialUser>();
  extAuthChanged = this.extAuthChangeSub.asObservable();
  isExternalAuth!: boolean;

  constructor(
    private cookieService: CookieService,
    private router: Router,
    private http: HttpClient,
    private externalAuthService: SocialAuthService
  ) {
    this.externalAuthService.authState.subscribe((user) => {
      this.GoogleLogin(user);
      this.isExternalAuth = true;
    });
  }

  async SignInWithGoogle() {

    await this.externalAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);

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
  async GoogleLogin(data: SocialUser) {
    var googleUser: ExternalProviderDto = {
      provider: data.provider,
      idToken: data.idToken
    };

    this.CookiesFactory(data.idToken);
    await this.http.post<AuthResponseDto>(`${environment.userHost}/googleLogin`, googleUser).toPromise();
  }
  SignOut() {
    this.cookieService.deleteAll();
    this.http.get(`${environment.userHost}/logout`).toPromise();

    return this.router.navigate(['/']);
  }
  GoogleSignOut() {
    this.isExternalAuth = false;
    this.externalAuthService.signOut();
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
