import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { lastValueFrom } from 'rxjs';
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
  datePipe: DatePipe = new DatePipe('en-US');

  constructor(
    private service: ShortServiceService,
    private router: Router

  ) { }

  ngOnInit() {

    this.form = new FormGroup({
      OriginalUrl: new FormControl('', Validators.required),
      CreatedOn: new FormControl(this.datePipe.transform(new Date(), 'dd MMM yyyy'), Validators.required)
    });
  }

  async ReduceUrl(value: UrlData) {

    var shortUrl = hash.hash(value.OriginalUrl);

    var dataUrl = {
      UrlId: Guid.create().toString(),
      OriginalUrl: value.OriginalUrl,
      ShortUrl: shortUrl,
      CreatedOn: value.CreatedOn,
    }

    this.spiner = true;
    const dataInfo = await this.service.CreateUrl(dataUrl);
    var result = await lastValueFrom(dataInfo).then(data => {
      this.currentShortUrl = data;
    });

    this.router.navigateByUrl('/shorturl');
  };
}
