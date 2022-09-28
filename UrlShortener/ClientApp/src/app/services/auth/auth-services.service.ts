import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import { Router } from '@angular/router';
import * as firebase from 'firebase/compat/app';
import { CookieService } from 'ngx-cookie-service';
import { environment } from '../../../environments/environment.prod';


@Injectable({
  providedIn: 'root'
})
export class AuthServicesService {
  currentDate!: Date;
  uid: any;
  jwt: any;
  email: any;
  user!: any;

  constructor(
    private afAuth: AngularFireAuth,
    private cookieService: CookieService,
    private router: Router,
    private http: HttpClient
  ) { }

  async SignInWithPopUp() {
    var provider = new firebase.default.auth.GoogleAuthProvider();

    return await firebase.default.auth().signInWithPopup(provider)
      .then((result) => {
        this.jwt = result.credential?.signInMethod;
        this.uid = result.user?.uid;
        this.email = result.user?.email;

        this.CookiesFactory(this.jwt, this.uid, this.email);

        this.router.navigateByUrl('/');

      }).catch(function (error) {
        var errorCode = error.code;
        var errorMessage = error.message;
        var email = error.email;
        var credential = error.credential;
      });
  }
  async SignInWithEmailAndPassword(email: string, password: string) {
    await this.afAuth.signInWithEmailAndPassword(email, password)
      .then((result) => {
        this.user = result.user;
      })
      .catch((error) => {
        window.alert(error.message);
      });
    var currentJwt = await this.GetIdToken();
    this.jwt = currentJwt!;

    this.CookiesFactory(this.jwt, this.user.uid, this.user.email);
    this.router.navigateByUrl('/');

  }
  async SignUpWithEmailAndPassword(email: string, password: string) {

    await this.afAuth.createUserWithEmailAndPassword(email, password)
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

    this.CookiesFactory(this.jwt, this.user.uid, this.user.email);

    this.CreateUser(this.user.email);

    this.router.navigateByUrl('/');
  }

  SignOut() {
    this.cookieService.deleteAll();

    return this.afAuth.signOut()
      .then(() => {
        this.router.navigate(['/']);
      });
  }

  async CreateUser(email: string) {
    return await this.http.post(`${environment.userHost}`, email).toPromise();
  };
  IsAuthenticated() {

    const checkUser = this.cookieService.get('JWT');

    if (checkUser) {
      return true;
    }
    return false;
  }
  CookiesFactory(jwt: string, uid: string, email: string) {

    this.currentDate = new Date();
    this.currentDate.setHours(this.currentDate.getHours() + 12);

    this.cookieService.set('JWT', jwt, { expires: this.currentDate, secure: true });
    this.cookieService.set('uid', uid, { expires: this.currentDate, secure: true });
    this.cookieService.set('email', email, { expires: this.currentDate, secure: true });
  }
  async GetIdToken() {
    return await firebase.default.auth().currentUser?.getIdToken();
  }
}
