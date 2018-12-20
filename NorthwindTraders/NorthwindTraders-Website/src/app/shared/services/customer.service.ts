import { Injectable } from '@angular/core';
import { Customer } from '../models/customer';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  customers: Customer[];

  constructor(private http: HttpClient) {
    this.customers = [{id: 'FOOBR',
      companyName: 'Fooberts Candies',
      contactName: 'John Foobert',
      contactTitle: 'CEO',
      address: '123 Somewhere St',
      city: 'London',
      region: 'Manchester',
      postalCode: '012-4356',
      country: 'UK',
      phone: '433-33-5647',
      fax: ''},
      {id: 'WWCFA',
        companyName: 'Willy Wonka\'s Candy Factory',
        contactName: 'Willy Wonka',
        contactTitle: 'Chief of Dreams',
        address: '321 Magical St',
        city: 'Wonka City',
        region: 'Birmingham',
        postalCode: '333-33-3333',
        country: 'WL',
        phone: '222-333-5555',
        fax: ''}];
   }

  getCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>('https://localhost:44344/api/customers');
  }

  addCustomer(customer: Customer) {
    this.customers.push(customer);
  }

  getCustomerById(id: string): Customer {
    const customer = this.customers.find(cust => cust.id === id);
    return customer;
  }

  updateCustomer(customer: Customer) {
    const custToUpdate = this.customers.find(cust => customer.id === cust.id);
    const index = this.customers.indexOf(custToUpdate);
    this.customers[index] = customer;
  }

  deleteCustomer(id: string) {
    this.customers = this.customers.filter(cust => cust.id !== id);
  }
}
