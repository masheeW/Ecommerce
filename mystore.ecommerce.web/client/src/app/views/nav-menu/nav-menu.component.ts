import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { BehaviorSubject, Observable } from "rxjs";
import { Store } from "../../services/store.service";

@Component({
    selector: 'nav-menu',
    templateUrl: 'nav-menu.component.html',
    styleUrls: []
})

export class NavMenu implements OnInit {

    LoginStatus$ = new BehaviorSubject<boolean>(false);

    constructor(private store: Store, private router: Router) {
    }


    ngOnInit(): void {
        this.store.globalStateChanged.subscribe((state) => {
            this.LoginStatus$.next(state.loggedInStatus);
        });

    }

}