import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UrlData } from 'src/app/models/UrlData';
import { ShortServiceService } from 'src/app/services/ShorteningURL/short-service.service';

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
  dateTimeFormat: string = 'dd MMM yyyy, HH:mm';
  urlRegexPattern: RegExp = /^(https|http:\/\/?)(?:\w+(?:\w+)?@)?[^\s\/]+(?:\d+)?(?:\/[\w#!:.?+=&%@\-\/]*)?$/;

  constructor(
    private service: ShortServiceService,
    private router: Router
  ) { }

  ngOnInit() {

    this.form = new FormGroup({
      OriginalUrl: new FormControl('', Validators.compose([
        Validators.required,
        Validators.pattern(this.urlRegexPattern)
      ])),
      CreatedOn: new FormControl(this.datePipe.transform(new Date(), this.dateTimeFormat), Validators.required)
    });
  }

  async CreateUrl(data: UrlData) {
    await this.service.CreateUrl(data)
      .then(data => {
        this.currentShortUrl = data;
      });
    this.spiner = true;

    this.router.navigateByUrl('/shorturl');
  };
}
