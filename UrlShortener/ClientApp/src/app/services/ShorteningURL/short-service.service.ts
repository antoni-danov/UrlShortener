import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { UrlData } from 'src/app/models/UrlData';

@Injectable({
  providedIn: 'root'
})
export class ShortServiceService {

  shortUrl!: string;

  constructor(
    private http: HttpClient,
  ) { }

  async CreateUrl(value: UrlData) {
    this.shortUrl = value.ShortUrl;
    return await this.http.post(`${environment.localhost}`, value);
  }
  async GetUrl(data: string) {

    var result = await this.http.get(`${environment.localhost}/${data}`);

    return result;
  }
  async GetShortUrl(data: string) {
    return await this.http.get(`${environment.localhost}/${data}`);
  }
}
