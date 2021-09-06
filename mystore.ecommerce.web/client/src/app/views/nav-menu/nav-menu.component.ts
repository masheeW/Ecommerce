import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { BehaviorSubject, Observable } from "rxjs";
import { StoreState } from "../../interfaces/store-state";
import { Store } from "../../services/store.service";
import { ProductCategory, SearchRequest } from "../../shared/Product";

@Component({
    selector: 'nav-menu',
    templateUrl: 'nav-menu.component.html',
    styleUrls: []
})

export class NavMenu implements OnInit {

    loginStatus!: boolean;
   
    constructor(public store: Store, public router: Router) {
    }

    ngOnInit() {
        this.store.loginStatus.subscribe(updatedval => {
            this.loginStatus = updatedval;
        });

        this.store.loadCategories()
            .subscribe(() => {

            });

        this.store.checkLoginStatus();
    }

    logout(): void {
        window.location.reload();
    }

    search(text: string): void {
        this.store.searchInfo.category = "";
        this.store.searchInfo.name = text;

        this.store.loadProducts(this.store.searchInfo)
            .subscribe(() => {

            });
        
    }

}