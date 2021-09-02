import { Component } from '@angular/core';
import { Store } from './services/store.service';

@Component({
  selector: 'the-shop',
  templateUrl: "app.component.html",
  styles: []
})
export class AppComponent {

    constructor(public store: Store) {
    }
}
