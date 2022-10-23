import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs/internal/firstValueFrom';
import { ShortServiceService } from 'src/app/services/ShorteningURL/short-service.service';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';


@Component({
  selector: 'app-short-url',
  templateUrl: './short-url.component.html',
  styleUrls: ['./short-url.component.css']
})
export class ShortUrlComponent implements OnInit{

  currentShortUrl: any;
  originalUrl!: any;
  environmentHost: string = environment.urlAddress;
  spiner: boolean = true;

  constructor(
    private service: ShortServiceService,
    private router: Router
  ) { }

  ngOnInit() {

    if (this.service.shortUrl !== undefined) {
      this.SetLocalStorage(this.service.shortUrl);
      this.GetUrl();
    } else {
      this.router.navigateByUrl("/");
    }

  }

  async GetUrl() {

    this.currentShortUrl = await this.service.temporaryValue;

    if (this.service.temporaryValue) {
      this.spiner = false;
      return this.currentShortUrl;
    } else {
      const result = await this.service.GetUrl(localStorage.getItem("shortUrl")!);
      await firstValueFrom(result).then(data => {
        this.currentShortUrl = data;
        this.spiner = false;
      });
    }

  }
  SetLocalStorage(shortUrl: string) {
    localStorage.setItem("shortUrl", shortUrl);
  }

  ShortAnotherUrl() {

    this.router.navigateByUrl('/');
  }
}
