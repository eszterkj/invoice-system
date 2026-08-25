export class Product {
  id!: number;
  name!: string;
  category!: string;
  unitPrice!: number;
  isHazardous!: boolean;
  isDiscountEligible!: boolean;
}

export interface CreateProductDto {
  name: string;
  category: string;
  unitPrice: number;
  isHazardous: boolean;
  isDiscountEligible: boolean;
}
