import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { UserService } from '../../services/User/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { ClipboardService } from 'ngx-clipboard';



@Component({
  selector: 'app-url-details',
  templateUrl: './url-details.component.html',
  styleUrls: ['./url-details.component.css']
})
export class UrlDetailsComponent implements OnInit {

  urlDetails: any;
  urlId!: string;
  environmentHost: string = environment.urlAddress;
  currentShortUrl: any;


  constructor(
    private userService: UserService,
    private location: Location,
    private activatedRoute: ActivatedRoute,
    private clipboard: ClipboardService,
    private router: Router
  ) { }

  ngOnInit() {
    this.urlId = this.activatedRoute.snapshot.paramMap.get('id')!;
    this.GetDetails(this.urlId);
    
  }

  async GetDetails(urlId: string) {

    await this.userService.GetById(urlId)
      .then((data) => {
        this.urlDetails = data;
      });

    
  }

  async CopyUrl(data: string) {

    var publicUrl = this.clipboard.copyFromContent(`${environment.urlAddress}` + data);
  }

  async goBack() {
    await this.location.back();
  }

}
