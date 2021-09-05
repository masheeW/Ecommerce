import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";

import { AppComponent } from './app.component';
import { Store } from './services/store.service';
import ProductListView from './views/productlist/productListView.component';
import { CartView } from './views/cart/cartView.component';
import router from './router';
import { CheckoutPage } from './pages/checkout/checkout.component';
import { ShopPage } from './pages/shop/shopPage.component';
import { LoginPage } from './pages/login/loginPage.component';
import { AuthActivator } from './services/authActivator.service';
import { FormsModule } from '@angular/forms';
import { RegisterPage } from './pages/register/register.component'; 
import { NavMenu } from './views/nav-menu/nav-menu.component';
import { PaymentPage } from './pages/payment/payment.component';
import { OrderPage } from './pages/order/order.component';
import { OrderDetailPage } from './pages/odetail/odetail.component';

@NgModule({
  declarations: [
        AppComponent,
        ProductListView,
        CartView,
        ShopPage,
        CheckoutPage,
        LoginPage,
        RegisterPage,
        NavMenu,
        PaymentPage,
        OrderPage,
        OrderDetailPage
  ],
  imports: [
      BrowserModule,
      HttpClientModule,
      router,
      FormsModule
  ],
    providers: [
        Store,
        AuthActivator
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
