import { Component, OnInit } from '@angular/core';
import { Customer } from '../../shared/models/customer';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.scss']
})
export class CustomerListComponent implements OnInit {

  customers: Customer[];

  constructor() { }

  ngOnInit() {
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

  countUpOne() {
    this.customers.push({
      id: 5,
      companyName: 'New Corp',
      contactName: 'New Contact',
      contactTitle: '',
      address: '321 Jump St',
      city: 'Boston',
      region: 'MA',
      postalCode: '11223',
      country: 'US',
      phone: '1-433-997-5647',
      fax: ''
    });
  }
}
