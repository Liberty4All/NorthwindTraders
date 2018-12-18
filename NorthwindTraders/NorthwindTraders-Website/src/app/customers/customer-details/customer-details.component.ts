import { Component, OnInit } from '@angular/core';
import { Customer } from '../../shared/models/customer';

@Component({
  selector: 'app-customer-details',
  templateUrl: './customer-details.component.html',
  styleUrls: ['./customer-details.component.scss']
})
export class CustomerDetailsComponent implements OnInit {
  customer: Customer;
  constructor() { }

  ngOnInit() {
    this.customer = {
      id: 20,
      companyName: 'Sweety\'s Sweets',
      contactName: 'Sweety McSweet',
      contactTitle: 'Owner',
      address: '678 Cherry Lane',
      city: 'Denver',
      region: 'CO',
      country: 'US',
      postalCode: '77777',
      phone: '876-555-1212',
      fax: ''
    };
  }

}
