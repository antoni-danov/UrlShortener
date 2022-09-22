import { AfterViewInit, Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs/internal/firstValueFrom';
import { ShortServiceService } from 'src/app/services/ShorteningURL/short-service.service';
import { ClipboardService } from 'ngx-clipboard';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';


@Component({
  selector: 'app-short-url',
  templateUrl: './short-url.component.html',
  styleUrls: ['./short-url.component.css']
})
export class ShortUrlComponent implements OnInit, AfterViewInit {

  currentShortUrl: any;
  originalUrl!: any;
  spiner: boolean = true;

  constructor(
    private service: ShortServiceService,
    private clipboard: ClipboardService,
    private router: Router
  ) { }

  ngOnInit() {

    if (this.service.shortUrl !== undefined) {
      this.SetLocalStorage(this.service.shortUrl);
    }

  }

  ngAfterViewInit() {
    this.GetUrl();
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

  async CopyUrl(data: string) {

    var publicUrl = this.clipboard.copyFromContent(`${environment.urlAddress}` + data);

    const originalLink = this.service.GetUrl(data);
  }

  ShortAnotherUrl() {

    this.router.navigateByUrl('/');
  }
}
