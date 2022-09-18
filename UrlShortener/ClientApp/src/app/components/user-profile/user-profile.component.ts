import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/User/user.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  urlData: any;

  constructor(
    private userService: UserService,
  ) { }

  ngOnInit() {

    this.userService.GetAll()
      .then((data) => {
        this.urlData = data;
      });
  }

  deleteUrl(urlId: string) {
    Swal.fire({
      title: 'Do you really want to delete this Url?',
      showDenyButton: true,
      cancelButtonColor: "red",
      confirmButtonColor: "green",
      confirmButtonText: 'Yes',
      denyButtonText: 'No'
    }).then((result) => {
      if (result.isConfirmed) {
        this.userService.DeleteUrl(urlId);
        Swal.fire({
          position: 'center',
          icon: 'success',
          title: 'The url record was deleted!',
          showConfirmButton: true,
          confirmButtonColor: "green"
        });
        this.ngOnInit();
      } else {
        Swal.fire({
          title: 'This time was close',
          icon: 'warning',
          showConfirmButton: true,
          confirmButtonColor: "green"
        })
      }
    });
  }

}
