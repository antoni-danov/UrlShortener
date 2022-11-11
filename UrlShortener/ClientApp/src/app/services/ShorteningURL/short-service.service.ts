import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { UrlData } from 'src/app/models/UrlData';
import { environment } from 'src/environments/environment';
import { AuthServicesService } from '../auth/auth-services.service';
var hash = require('jhash');


@Injectable({
  providedIn: 'root'
})
export class ShortServiceService {

  shortUrl!: string;
  temporaryValue!: Object;

  constructor(
    private http: HttpClient,
    private cookies: CookieService
  ) { }

  async CreateUrl(data: UrlData) {
    var object = this.formatUrlData(data);
    var result = await this.http.post(`${environment.localhost}`, object).toPromise();
    this.temporaryValue = result!;

    return result;

  }

  async GetAll(): Promise<UrlData[] | undefined> {
    var params = this.cookies.get('uid');
    return await this.http.get<UrlData[]>(`${environment.localhost}/search/all/${params}`).toPromise();
  }

  async GetById(urlId: string): Promise<UrlData | undefined> {
    return await this.http.get<UrlData>(`${environment.localhost}/urlById/${urlId}`).toPromise();
  }

  async GetUrl(data: string) {

    var result = await this.http.get(`${environment.localhost}/${data}`);

    return result;
  }

  DeleteUrl(urlId: number) {
    return this.http.delete(`${environment.localhost}/delete/${urlId}`).toPromise();
  }

  formatUrlData(data: UrlData): UrlData {
    var shortUrl = hash.hash(data.OriginalUrl);
    var actualUid = this.cookies.get('Uid');

    var dataUrl = {
      OriginalUrl: data.OriginalUrl,
      ShortUrl: shortUrl,
      CreatedOn: data.CreatedOn,
      Uid: actualUid.length != 0 ? actualUid : "N/A" 
    };
    localStorage.setItem("shortUrl", data.ShortUrl);

    this.shortUrl = localStorage.getItem("shortUrl")!;

    return dataUrl;
  } 
}
