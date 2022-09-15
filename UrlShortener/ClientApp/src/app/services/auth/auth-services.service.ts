import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import * as firebase from 'firebase/compat/app';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';


@Injectable({
  providedIn: 'root'
})
export class AuthServicesService {
  currentDate!: Date;
  uid!: string;
  jwt!: string;
  email!: string;
  user!: any;

  constructor(
    private afAuth: AngularFireAuth,
    private cookieService: CookieService,
    private router: Router
  ) { }

  async SignInWithEmailAndPassword(email: string, password: string) {
    await this.afAuth.signInWithEmailAndPassword(email, password)
      .then((result) => {
        this.user = result.user;
      })
      .catch((error) => {
        window.alert(error.message);
      });
    var currentJwt = await this.getIdToken();
    this.jwt = currentJwt!;

    this.cookiesFactory(this.jwt, this.user.uid, this.user.email);

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

    this.cookiesFactory(this.jwt, this.user.uid, this.user.email);

    this.router.navigateByUrl('/');
  }
  async SignOut() {
    this.cookieService.deleteAll();

    return await this.afAuth.signOut()
      .then(() => {
        this.router.navigate(['/']);
      });
  }

  isAuthenticated() {

    const checkUser = this.cookieService.get('JWT');

    if (checkUser) {
      return true;
    }
    return false;
  }
  cookiesFactory(jwt: string, uid: string, email: string) {

    this.currentDate = new Date();
    this.currentDate.setHours(this.currentDate.getHours() + 12);

    this.cookieService.set('JWT', jwt, { expires: this.currentDate, secure: true });
    this.cookieService.set('uid', uid, { expires: this.currentDate, secure: true });
    this.cookieService.set('email', email, { expires: this.currentDate, secure: true });
  }
  async getIdToken() {
    return await firebase.default.auth().currentUser?.getIdToken();
  }
}
