import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { UserService } from '../../services/User/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import Swal from 'sweetalert2';
import { ShortServiceService } from '../../services/ShorteningURL/short-service.service';




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
    private shortService: ShortServiceService,
    private location: Location,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.urlId = this.activatedRoute.snapshot.paramMap.get('id')!;
    this.GetDetails(this.urlId);

  }

  async GetDetails(urlId: string) {

    await this.shortService.GetById(urlId)
      .then((data) => {
        this.urlDetails = data;
      });


  }

  deleteUrl(urlId: number) {
    if (confirm("Are you sure")) {
      this.shortService.DeleteUrl(urlId);
      alert("Record deleted");
      this.router.navigateByUrl('/profile');
    } else {
      alert("Not this time");
    }
  }


  async goBack() {
    await this.location.back();
  }

}
