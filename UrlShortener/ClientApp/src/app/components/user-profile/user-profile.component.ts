import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/User/user.service';
import Swal from 'sweetalert2';
import { environment } from '../../../environments/environment';


@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  urlData!: Array<any>;
  numberOfRecords!: number;
  host: string = environment.urlAddress;

  constructor(
    private userService: UserService,
  ) { }

  ngOnInit() {
    this.GetAllUrls();
  }

  async GetAllUrls() {

   return await this.userService.GetAll()
     .then((data) => {
       this.urlData = data!;
       this.numberOfRecords = this.urlData!.length;
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
        this.ngOnInit();
        Swal.fire(
          'Deleted!',
          'Your Url has been deleted.',
          'success'
        );
        
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire(
          'Cancelled',
          'Your Url is safe!',
          'error'
        );
      }
    });
  }

}
