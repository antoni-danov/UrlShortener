import { Location } from '@angular/common';

export class utils {
  constructor(
    private location: Location
  ) { }

  goBack() {
    this.location.back();
  }
}
