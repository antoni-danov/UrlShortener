import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { environment } from '../../../environments/environment';
import { UrlData } from '../../models/UrlData';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  status!: string;

  constructor(
    private http: HttpClient,
    private cookies: CookieService
  ) { }

  async GetAll(): Promise<UrlData[] | undefined>{
    var params = this.cookies.get('uid');
     return await this.http.get<UrlData[]>(`${environment.userHost}/search/all/${params}`).toPromise();
  }
  async GetById(urlId: string): Promise<UrlData | undefined>{
    return await this.http.get<UrlData>(`${environment.userHost}/${urlId}`).toPromise();
  }
  DeleteUrl(urlId: string) {
    return this.http.delete(`${environment.userHost}/delete/${urlId}`).toPromise();
  }
}
