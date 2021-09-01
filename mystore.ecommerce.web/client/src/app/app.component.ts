import { Component } from '@angular/core';
import { Store } from './services/store.service';

@Component({
  selector: 'the-shop',
  templateUrl: "app.component.html",
  styles: []
})
export class AppComponent {
    isAuthenticated = this.store.isAuthenticated;
    title = 'Shop';


    constructor(public store: Store) {
    }
}
