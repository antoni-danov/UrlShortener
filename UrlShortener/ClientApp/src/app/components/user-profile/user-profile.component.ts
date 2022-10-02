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
    if (confirm("Are you sure")) {
      this.userService.DeleteUrl(urlId);
      alert("Record deleted");
      this.ngOnInit();
    } else {
      alert("Not this time");
    }
  }

}
