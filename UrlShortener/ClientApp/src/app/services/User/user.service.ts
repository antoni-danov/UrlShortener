import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { UrlDataDTO } from '../../models/UrlDataDTO';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private http: HttpClient
  ) { }

  async GetAll() {
    return await this.http.get(`${environment.userHost}`).toPromise();
  }
  async GetById() {

  }
  async EditUrl() {

  }
  async DeleteUrl() {

  }
}
