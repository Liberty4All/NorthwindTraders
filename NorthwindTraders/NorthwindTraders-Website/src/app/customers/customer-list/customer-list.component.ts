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

  delete(id: string) {
    this.customerService.deleteCustomer(id);
    this.customers = this.customerService.getCustomers();
  }
}
