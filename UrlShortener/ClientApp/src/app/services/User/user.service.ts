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

   GetAll() {
    return this.http.get(`${environment.userHost}`).toPromise();
  }
  async GetById() {

  }
  async EditUrl() {

  }
  async DeleteUrl(urlId: string) {
    return await this.http.delete(`${environment.userHost}/${urlId}`).toPromise();
  }
}
