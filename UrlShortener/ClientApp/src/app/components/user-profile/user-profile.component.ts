import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/User/user.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  urlData: any;

  constructor(
    private userService: UserService,
    private route: Router
  ) { }

  ngOnInit() {

    this.userService.GetAll()
      .then((data) => {
        this.urlData = data;
      });
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

}
