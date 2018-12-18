import { Component, OnInit } from '@angular/core';
import { Customer } from '../../shared/models/customer';
import { CustomerService } from '../../shared/services/customer.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-customer-details',
  templateUrl: './customer-details.component.html',
  styleUrls: ['./customer-details.component.scss']
})
export class CustomerDetailsComponent implements OnInit {
  customer: Customer;
  constructor(
    private route: ActivatedRoute,
    private customerService: CustomerService
  ) { }

  ngOnInit(): void {
    this.getCustomer();
  }

  getCustomer(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.customer = this.customerService.getCustomerById(id);
  }
}
