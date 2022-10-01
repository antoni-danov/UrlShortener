import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { UrlData } from 'src/app/models/UrlData';
import { environment } from 'src/environments/environment';
var hash = require('jhash');


@Injectable({
  providedIn: 'root'
})
export class ShortServiceService {

  shortUrl!: any;
  temporaryValue!: any;

  constructor(
    private http: HttpClient,
    private cookiesService: CookieService
  ) { }

  async CreateUrl(data: UrlData) {
    var object = this.formatUrlData(data);
    var result = await this.http.post(`${environment.localhost}`, object).toPromise();
    this.temporaryValue = result;

    return result;

  }

  async GetUrl(data: string) {

    var result = await this.http.get(`${environment.localhost}/${data}`);

    return result;
  }

  formatUrlData(data: UrlData): UrlData {
    var shortUrl = hash.hash(data.OriginalUrl);
    var actualUid = this.cookiesService.get('uid');

    var dataUrl = {
      OriginalUrl: data.OriginalUrl,
      ShortUrl: shortUrl,
      CreatedOn: data.CreatedOn,
      UserId: actualUid
    };

    this.shortUrl = localStorage.setItem("shortUrl", data.ShortUrl);

    return dataUrl;
  } 
}
