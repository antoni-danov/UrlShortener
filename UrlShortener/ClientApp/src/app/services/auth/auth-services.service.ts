import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/compat/auth';

@Injectable({
  providedIn: 'root'
})
export class AuthServicesService {

  constructor(
    private afAuth: AngularFireAuth
  ) { }

  SignUpWithEmailAndPassword(email: string, password: string) {

    return this.afAuth.createUserWithEmailAndPassword(email, password)
      .then((result) => {
        const user = result.user;
      })
      .catch((error) => {
        const errorCode = error.code;
        const errorMessage = error.message;
      });
  }
}
