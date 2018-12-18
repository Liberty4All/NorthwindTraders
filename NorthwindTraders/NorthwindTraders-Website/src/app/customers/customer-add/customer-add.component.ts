import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../../shared/services/customer.service';
import { Customer } from '../../shared/models/customer';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-customer-add',
  templateUrl: './customer-add.component.html',
  styleUrls: ['./customer-add.component.scss']
})
export class CustomerAddComponent implements OnInit {

  customerForm = new FormGroup({
    id: new FormControl(''),
    companyName: new FormControl(''),
    contactName: new FormControl(''),
    contactTitle: new FormControl(''),
    address: new FormControl(''),
    city: new FormControl(''),
    region: new FormControl(''),
    postalCode: new FormControl(''),
    country: new FormControl(''),
    phone: new FormControl(''),
    fax: new FormControl('')
  });

  constructor(private customerService: CustomerService,
    private router: Router) { }

  ngOnInit() {
  }

  save() {
    const customer = this.customerForm.value;
    this.customerService.addCustomer(customer);
    this.customerForm.reset();
    this.router.navigateByUrl('customers');
  }
}
