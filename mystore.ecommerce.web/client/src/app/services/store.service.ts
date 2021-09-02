import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { map } from "rxjs/operators";
import { StoreState } from "../interfaces/store-state";
import { LoginRequest, LoginResults, UserRegistrationReq } from "../shared/LoginResults";
import { Order, OrderItem } from "../shared/Order";
import { Product } from "../shared/Product";

@Injectable()
export class Store {
 

    private _loginStatus = new BehaviorSubject<boolean>(this.checkLoginStatus());
    readonly loginStatus = this._loginStatus.asObservable();

    constructor(private http: HttpClient) { }
    public products: Product[] = [];
    public order: Order = new Order();
    public token = "";
    public expiration = new Date();
    public isAuthenticated!: boolean;
    public message = "";
    public lastOrderNumber = "";

    get getLogin() {
        return this._loginStatus.asObservable();
    }

    checkLoginStatus(): boolean {
        return this.isAuthenticated;
    }

    loadProducts(): Observable<void>{
        return this.http.get<[]>("/api/products")
            .pipe(map(data => {
                this.products = data;
                return;
            }));
    }

    get loginRequired(): boolean {
        return this.token.length == 0 || this.expiration > new Date();
    }

    login(creds: LoginRequest) {
        return this.http.post<LoginResults>("/account/createtoken", creds)
            .pipe(map(data => {
                this.token = data.token;
                this.expiration = data.expiration;

                this.isAuthenticated = data.IsAuthenticated;

                this._loginStatus.next(Object.assign({}, this.isAuthenticated));
            }));

    }


    register(userInfo: UserRegistrationReq): Observable<void> {
        return this.http.post<LoginResults>("/account/Register", userInfo)
            .pipe(map(data => {
                this.token = data.token;
                this.expiration = data.expiration;

                this.isAuthenticated = data.IsAuthenticated;

                this._loginStatus.next(Object.assign({}, this.isAuthenticated));
            }));

    }

    addToOrder(product: Product) {

        let item: OrderItem | undefined;

        item = this.order.orderItems.find(o => o.productId === product.id);

        if (item) {
            item.quantity++;
            this.order.totalAmount = this.order.totalAmount + item.unitPrice;
            item.totalPrice = item.totalPrice + item.unitPrice;

        }
        else {
            const item = new OrderItem();
            item.productId = product.id;
            item.product = product;
            item.unitPrice = product.price;
            item.quantity = 1;
            item.totalPrice = product.price;
            this.order.totalAmount = this.order.totalAmount + item.totalPrice;
            this.order.orderItems.push(item);
        }
    }

    removeFromCart(item: Product) {
        let oitem: OrderItem | undefined;

        oitem = this.order.orderItems.find(o => o.productId === item.id);

        if (oitem) {
            this.order.totalAmount = this.order.totalAmount - oitem?.totalPrice;

            this.order.orderItems = this.order.orderItems.filter(itm => itm !== oitem);
        }
      
    }

    checkout() {
        const headers = new HttpHeaders().set("Authorization", `Bearer ${this.token}`);

        return this.http.post("/api/orders", this.order, {
            headers: headers
        })
            .pipe(map(() => {
                this.lastOrderNumber = this.order.orderNumber;
                this.order = new Order();
            }));
    }
}