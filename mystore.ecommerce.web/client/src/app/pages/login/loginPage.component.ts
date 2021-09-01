import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { BehaviorSubject } from "rxjs";
import { Store } from "../../services/store.service";
import { LoginRequest } from "../../shared/LoginResults";

@Component({
    selector: "login-page",
    templateUrl: "loginPage.component.html"
})
export class LoginPage implements OnInit {
   
    LoginStatus$ = new BehaviorSubject<boolean>(false);

    public creds: LoginRequest = {
        username: "",
        password: ""
    }

    public errorMessage = "";

    constructor(public store: Store, private router: Router) { }


    ngOnInit(): void {
        this.store.globalStateChanged.subscribe((state) => {
            this.LoginStatus$.next(state.loggedInStatus);
        });


        if (this.LoginStatus$.getValue()) {
            this.router.navigate(['/']);
        }
    }


    onLogin() {
        this.store.login(this.creds)
            .subscribe(() => {
                 
                this.store.isAuthenticated = true;
                //success
                if (this.store.order.orderItems.length > 0) {
                    this.router.navigate(["checkout"]);
                }
                else {
                    this.router.navigate([""]);
                }
            }, error => {
                console.log(error);
                this.errorMessage = "Failed to login";
            });
    }


}