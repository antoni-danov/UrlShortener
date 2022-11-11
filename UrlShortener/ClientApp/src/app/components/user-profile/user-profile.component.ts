import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { environment } from '../../../environments/environment';
import { ShortServiceService } from '../../services/ShorteningURL/short-service.service';


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
    private shortService: ShortServiceService
  ) { }

  ngOnInit() {
    this.GetAllUrls();
  }

  async GetAllUrls() {

    return await this.shortService.GetAll()
      .then((data) => {
        this.urlData = data!;
        this.numberOfRecords = this.urlData!.length;
      });
  }

  deleteUrl(urlId: number) {
    if (confirm("Are you sure")) {
      this.shortService.DeleteUrl(urlId);
      alert("Record deleted");
      this.ngOnInit();
    } else {
      alert("Not this time");
    }
  }

}
