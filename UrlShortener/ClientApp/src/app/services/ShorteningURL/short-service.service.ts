import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UrlData } from 'src/app/models/UrlData';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ShortServiceService {

  shortUrl!: any;
  temporaryValue!: any;

  constructor(
    private http: HttpClient
  ) { }

  async CreateUrl(value: UrlData){

    this.shortUrl = localStorage.setItem("shortUrl", value.ShortUrl);

    var result = await this.http.post(`${environment.localhost}`, value);
    this.temporaryValue = result.toPromise();

    return result;

  }

  async GetUrl(data: string) {

    var result = await this.http.get(`${environment.localhost}/${data}`);

    return result;
  }

  async GetShortUrl(data: string) {
    return await this.http.get(`${environment.localhost}/${data}`);
  }
}
