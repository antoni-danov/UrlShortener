import { AfterViewInit, Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs/internal/firstValueFrom';
import { UrlData } from 'src/app/models/UrlData';
import { ShortServiceService } from 'src/app/services/ShorteningURL/short-service.service';
import { ClipboardService } from 'ngx-clipboard';
import { Router } from '@angular/router';


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

    const result = await this.service.GetUrl(localStorage.getItem("shortUrl")!);

    var dataUrl = await firstValueFrom(result).then(data => {
      this.currentShortUrl = data;
      this.spiner = false;
    });

    return this.currentShortUrl;

  }
  SetLocalStorage(shortUrl: string) {
    localStorage.setItem("shortUrl", shortUrl);
  }
  async CopyUrl(data: string) {

    var publicUrl = this.clipboard.copyFromContent("https://localhost:44347/" + data);

    const originalLink = this.service.GetShortUrl(data);
  }
  ShortAnotherUrl() {

    this.router.navigateByUrl('/');
  }
}
