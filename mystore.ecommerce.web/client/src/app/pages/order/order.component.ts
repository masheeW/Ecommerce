import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Store } from "../../services/store.service";
import { Order } from "../../shared/Order";

@Component({
    selector: "order",
    templateUrl: "order.component.html"
})

export class OrderPage implements OnInit {
    public errorMessage = "";
    public orders = [] as any;

    constructor(public store: Store, private router: Router) {
    }

    ngOnInit(): void {
        this.store.loadOrders()
            .subscribe(() => {
          
            });
    }

    onOrderView(order: Order) {
        this.errorMessage = "";
        this.store.viewOrder(order)
            .subscribe(() => {
                this.router.navigate(["odetail"]);
            }, (err: any) => {
                this.errorMessage = `Failed to checkout: ${err}`;
            })
    }
}