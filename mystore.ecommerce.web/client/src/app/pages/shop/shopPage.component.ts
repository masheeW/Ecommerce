import { Component } from "@angular/core";
import { Store } from "../../services/store.service";
import { SearchRequest } from "../../shared/Product";

@Component({
    selector: "shop-page",
    templateUrl: "shopPage.component.html",
    styleUrls: []
})
export class ShopPage {
    title = 'Welcome to My Store';

    constructor(public store: Store) {
    }

    ngOnInit(): void {
        this.store.loadProducts(this.store.searchInfo)
            .subscribe(() => {

            });
    }
}