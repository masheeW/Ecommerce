import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { Store } from "../../services/store.service";
import { UserRegistrationReq } from "../../shared/LoginResults";


@Component({
    selector: "register-page",
    templateUrl: "register.component.html"
})

export class RegisterPage {
    constructor(public store: Store, private router: Router) { }

    public userInfo: UserRegistrationReq = {
        firstname: "",
        lastname: "",
        email: "",
        password: "",
        confirmpassword: ""
    }

    public errorMessage = "";
    public successMessage = "";


    onRegister() {
        this.store.register(this.userInfo)
            .subscribe(() => {
                this.store.isAuthenticated = true;
                this.successMessage = "Successfully Registered";
                //success
                //if (this.store.order.orderItems.length > 0) {
                //    this.router.navigate(["checkout"]);
                //}
                //else {
                //    this.router.navigate([""]);
                //}
            }, error => {
                console.log(error);
                this.errorMessage = "Failed to register";
            });
    }
}