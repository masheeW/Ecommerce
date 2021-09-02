import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Store } from "../../services/store.service";

@Component({
    selector: "payment",
    templateUrl: "payment.component.html",
    styleUrls: ['payment.component.css']
})
export class PaymentPage implements OnInit{

    public orderNumber = "";

    constructor(public store: Store, private router: Router) {
    }

    ngOnInit() {
        this.orderNumber = this.store.lastOrderNumber;
    }
}