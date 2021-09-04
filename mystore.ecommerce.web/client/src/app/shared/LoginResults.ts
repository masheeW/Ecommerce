export class LoginResults{
    token!: string;
    expiration!: Date;
    IsAuthenticated!: boolean;
}


export class LoginRequest {
    username!: string;
    password!: string;
}

export class UserRegistrationReq {
    firstname!: string;
    lastname!: string;
    email!: string;
    password!: string;
    confirmpassword!: string;
}