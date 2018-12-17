import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 1; /*'NorthwindTraders by William Wallace'*/
  items: string[] = ['Item One', 'Item Two'];

  countUpOne() {
    this.title++;
  }
}
