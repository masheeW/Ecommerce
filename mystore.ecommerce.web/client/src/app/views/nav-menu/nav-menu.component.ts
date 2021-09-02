import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { BehaviorSubject, Observable } from "rxjs";
import { StoreState } from "../../interfaces/store-state";
import { Store } from "../../services/store.service";

@Component({
    selector: 'nav-menu',
    templateUrl: 'nav-menu.component.html',
    styleUrls: []
})

export class NavMenu implements OnInit {

    loginStatus!: boolean;

    constructor(private store: Store, private router: Router) {
    }

    ngOnInit() {
        this.store.loginStatus.subscribe(updatedval => {
            this.loginStatus = updatedval;
        });

        //this.loginStatus = this.store.loginStatus;

       // this.loginStatus = this.store.loginStatus;
        this.store.checkLoginStatus();
    }

    logout(): void {
        window.location.reload();
    }

}