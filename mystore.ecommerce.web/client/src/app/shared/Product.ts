export class Product {
    id!: string;
    productName!: string;
    category!: string;
    size!: string;
    price!: number;
    imageName!: string;
    createddate!: Date;
    createdby!: string;
    updateddate?: any;
    updatedby?: any;
}

export class ProductCategory {
    id!: string;
    categoryName!: string;
}

export class SearchRequest {
    category!: string;
    name!: string;
}