import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { UserService } from '../../services/User/user.service';
import { ActivatedRoute } from '@angular/router';
import { environment } from '../../../environments/environment';
import Swal from 'sweetalert2';




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
    private activatedRoute: ActivatedRoute
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

  deleteUrl(urlId: string) {

    Swal.fire({
      title: 'Are you sure want to delete this Url?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it'
    }).then((result) => {
      if (result.value) {
        this.userService.DeleteUrl(urlId);
        Swal.fire(
          'Deleted!',
          'Your Url has been deleted.',
          'success'
        )
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire(
          'Cancelled',
          'Your Url is safe!',
          'error'
        )
      }
    });
  }


  async goBack() {
    await this.location.back();
  }

}
