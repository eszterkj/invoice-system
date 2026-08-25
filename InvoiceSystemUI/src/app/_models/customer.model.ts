export class Customer{
  id!: number;
  name!: string;
  country!: string;
  address!: string;
}

export interface CreateCustomerDto {
  name: string;
  country: string;
  address: string;
}
