import { Component, OnInit } from '@angular/core';
import { Customer } from '../../shared/models/customer';
import { CustomerService } from '../../shared/services/customer.service';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.scss']
})
export class CustomerListComponent implements OnInit {
  customers: Customer[];

  constructor(private customerService: CustomerService) { }

  ngOnInit() {
    this.customers = this.customerService.getCustomers();
  }

  addACustomer() {
    const customer: Customer = {
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
    };
    this.customerService.addCustomer(customer);
  }
}
