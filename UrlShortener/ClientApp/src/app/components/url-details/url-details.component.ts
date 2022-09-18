import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';


@Component({
  selector: 'app-url-details',
  templateUrl: './url-details.component.html',
  styleUrls: ['./url-details.component.css']
})
export class UrlDetailsComponent implements OnInit {

  constructor(
    private location: Location
  ) { }

  ngOnInit() {
  }

  async goBack() {
    await this.location.back();
  }

}
