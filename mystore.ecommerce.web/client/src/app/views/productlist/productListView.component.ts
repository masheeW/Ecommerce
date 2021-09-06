import { Component, OnInit } from "@angular/core";
import { Store } from "../../services/store.service";
import { SearchRequest } from "../../shared/Product";

@Component({
    selector: 'product-list',
    templateUrl: "productListView.component.html",
    styleUrls: ["productListView.component.css"]
})
export default class ProductListView implements OnInit {

    public products = [] as any;
 
    constructor(public store: Store) {
    }

    ngOnInit(): void {
        this.store.loadProducts(this.store.searchInfo)
            .subscribe(() => {

            });
    }
}