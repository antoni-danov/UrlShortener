import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { UrlData } from '../../models/UrlData';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  status!: string;

  constructor(
    private http: HttpClient
  ) { }

   async GetAll(): Promise<UrlData[] | undefined>{
    return await this.http.get<UrlData[]>(`${environment.userHost}`).toPromise();
  }
  async GetById(urlId: string): Promise<UrlData | undefined>{
    return await this.http.get<UrlData>(`${environment.userHost}/${urlId}`).toPromise();
  }
  DeleteUrl(urlId: string) {
    return this.http.delete(`${environment.userHost}/delete/${urlId}`).toPromise();
  }
}
