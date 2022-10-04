import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import { Router } from '@angular/router';
import * as firebase from 'firebase/compat/app';
import { CookieService } from 'ngx-cookie-service';
import { environment } from '../../../environments/environment.prod';
import { Cookie } from '../../models/Cookie';
import { User } from '../../models/User';


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
  cookie!: Cookie;

  constructor(
    private afAuth: AngularFireAuth,
    private cookieService: CookieService,
    private router: Router,
    private http: HttpClient
  ) { }

  async SignInWithPopUp() {
    var provider = new firebase.default.auth.GoogleAuthProvider();

    await firebase.default.auth().signInWithPopup(provider)
      .then((result) => {
        this.jwt = result?.credential?.signInMethod!;
        this.uid = result.user?.uid;
        this.email = result?.user?.email!;
      }).catch(function (error) {
        var errorCode = error.code;
        var errorMessage = error.message;
        var email = error.email;
        var credential = error.credential;
      });

    this.CreateUser(this.uid);

    this.cookie = {
      JWT: this.jwt,
      Uid: this.uid,
      Email: this.email,
      RememberMe: false
    };
    this.CookiesFactory(this.cookie);

    this.router.navigateByUrl('/');
  }
  async SignInWithEmailAndPassword(userData: any) {
    await this.afAuth.signInWithEmailAndPassword(userData.email, userData.password)
      .then((result) => {
        this.user = result.user;
      })
      .catch((error) => {
        window.alert(error.message);
      });
    var currentJwt = await this.GetIdToken();
    this.jwt = currentJwt!;

   this.cookie = {
      JWT: this.jwt,
      Uid: this.user.uid,
      Email: this.user.email,
      RememberMe: userData.checkbox
    };

    this.CookiesFactory(this.cookie);
    this.router.navigateByUrl('/');

  }
  async SignUpWithEmailAndPassword(userData: any) {

    await this.afAuth.createUserWithEmailAndPassword(userData.email, userData.password)
      .then((result) => {
        this.user = result.user;
      })
      .catch((error) => {
        const errorCode = error.code;
        const errorMessage = error.message;
      });

    await firebase.default.auth().currentUser?.getIdToken()
      .then(data => {
        this.jwt = data;
      });

    this.cookie = {
      JWT: this.jwt,
      Uid: this.user.uid,
      Email: this.user.email,
      RememberMe: userData.checkbox
    };

    this.CookiesFactory(this.cookie);
    this.CreateUser(this.user.uid);

    this.router.navigateByUrl('/');
  }

  SignOut() {
    this.cookieService.deleteAll();

    return this.afAuth.signOut()
      .then(() => {
        this.router.navigate(['/']);
      });
  }

  async CreateUser(uid: User){
    var user = { Uid: uid };
    var createdUser = await this.http.post(`${environment.userHost}`, user).toPromise();

    return createdUser;
  };
  IsAuthenticated() {

    const checkUser = this.cookieService.get('JWT');

    if (checkUser) {
      return true;
    }
    return false;
  }
  CookiesFactory(cookieData: Cookie) {

    this.currentDate = new Date();
    this.currentDate.setHours(this.currentDate.getHours() + 12);

    this.cookieService.set('JWT', cookieData.JWT, { expires: this.currentDate, secure: true });
    this.cookieService.set('uid', cookieData.Uid, { expires: this.currentDate, secure: true });
    this.cookieService.set('email', cookieData.Email, { expires: this.currentDate, secure: true });

    if (cookieData.RememberMe == true) {
      this.currentDate.setHours(this.currentDate.getFullYear() + 12);
    }
  }
  async GetIdToken() {
    return await firebase.default.auth().currentUser?.getIdToken();
  }
}
