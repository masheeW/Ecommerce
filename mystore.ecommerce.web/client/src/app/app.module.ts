import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";

import { AppComponent } from './app.component';
import { Store } from './services/store.service';
import ProductListView from './views/productListView.component';
import { CartView } from './views/cart/cartView.component';
import router from './router';
import { CheckoutPage } from './pages/checkout/checkout.component';
import { ShopPage } from './pages/shop/shopPage.component';
import { LoginPage } from './pages/login/loginPage.component';
import { AuthActivator } from './services/authActivator.service';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
        AppComponent,
        ProductListView,
        CartView,
        ShopPage,
        CheckoutPage,
        LoginPage
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
