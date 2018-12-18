import { Injectable } from '@angular/core';
import { Customer } from '../models/customer';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  customers: Customer[];

  constructor() {
    this.customers = [{id: 1,
      companyName: 'Fooberts Candies',
      contactName: 'John Foobert',
      contactTitle: 'CEO',
      address: '123 Somewhere St',
      city: 'London',
      region: 'Manchester',
      postalCode: '012-4356',
      country: 'UK',
      phone: '433-33-5647',
      fax: ''}];
   }

  getCustomers(): Customer[] {
    return this.customers;
  }

  addCustomer(customer: Customer) {
    this.customers.push(customer);
  }
}
