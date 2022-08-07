import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { firstValueFrom } from 'rxjs/internal/firstValueFrom';
import { UrlData } from 'src/app/models/UrlData';
import { ShortServiceService } from 'src/app/services/ShorteningURL/short-service.service';
var hash = require('jhash');

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  form!: any;
  currentShortUrl!: any;
  originalUrl!: any;
  spiner: boolean = false;

  constructor(
    private service: ShortServiceService,
    private router: Router

  ) { }

  ngOnInit() {
    this.form = new FormGroup({
      OriginalUrl: new FormControl('', Validators.required),
      CreatedOn: new FormControl(formatDate(new Date(), 'dd/MM/YYYY HH:MM:SS', 'en'), Validators.required)
    });
  }

  async ReduceUrl(value: UrlData) {

    var longUrl = value.OriginalUrl;
    var shortUrl = hash.hash(longUrl);

    var dataUrl = {
      OriginalUrl: value.OriginalUrl,
      CreatedOn: value.CreatedOn,
      ShortUrl: shortUrl
    }

    this.spiner = true;
    const dataInfo = await this.service.CreateUrl(dataUrl);
    // this.form.reset();
    var result = await lastValueFrom(dataInfo).then(data => {
      this.currentShortUrl = data;
    });

    this.router.navigateByUrl('/short');
  };
}
