import { RouterModule } from "@angular/router";
import { CheckoutPage } from "../pages/checkout/checkout.component";
import { LoginPage } from "../pages/login/loginPage.component";
import { RegisterPage } from "../pages/register/register.component";
import { ShopPage } from "../pages/shop/shopPage.component";
import { AuthActivator } from "../services/authActivator.service";

const routes = [
    { path: "", component: ShopPage },
    { path: "checkout", component: CheckoutPage, canActivate: [AuthActivator] },
    { path: "login", component: LoginPage },
    { path: "register", component: RegisterPage },
    { path: "**", redirectTo: "/"}
];

const router = RouterModule.forRoot(routes, {
    useHash: false
});

export default router;