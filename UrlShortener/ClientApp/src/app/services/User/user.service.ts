import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  status!: string;

  constructor(
    private http: HttpClient
  ) { }

   async GetAll() {
    return await this.http.get(`${environment.userHost}`).toPromise();
  }
  async GetById(urlId: string){
    return await this.http.get(`${environment.userHost}/${urlId}`).toPromise();
  }
  async DeleteUrl(urlId: string) {
    return await this.http.delete(`${environment.userHost}/delete/${urlId}`).toPromise();
  }
}
