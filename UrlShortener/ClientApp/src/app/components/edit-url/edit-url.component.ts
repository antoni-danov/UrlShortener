import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { UserService } from '../../services/User/user.service';


@Component({
  selector: 'app-edit-url',
  templateUrl: './edit-url.component.html',
  styleUrls: ['./edit-url.component.css']
})
export class EditUrlComponent implements OnInit {

  constructor(
    private userService: UserService,
    private location: Location
  ) { }

  ngOnInit() {
  }

  deleteUrl(urlId: string) {
    if (confirm("Do you really want to delete this Url?")) {
      this.userService.DeleteUrl(urlId);
      alert("The Record was deleted");
      this.ngOnInit();
    } else {
      alert("This time was close!");
    }
  }

  async backToDetails() {
    await this.location.back();
  }

}
