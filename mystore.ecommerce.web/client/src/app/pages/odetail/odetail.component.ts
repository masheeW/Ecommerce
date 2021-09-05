import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Store } from "../../services/store.service";

@Component({
    selector: "odetail",
    templateUrl: "odetail.component.html"
})

export class OrderDetailPage {
    public errorMessage = "";

    constructor(public store: Store, private router: Router) {
    }

}