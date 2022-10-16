import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  status!: string;

  constructor(
    private http: HttpClient,
    private cookies: CookieService
  ) { }

}
