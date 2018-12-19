import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerService } from '../../shared/services/customer.service';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-customer-update',
  templateUrl: './customer-update.component.html',
  styleUrls: ['./customer-update.component.scss']
})
export class CustomerUpdateComponent implements OnInit {
  id: string;
  customerForm = new FormGroup({
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

  constructor(
    private route: ActivatedRoute,
    private customerService: CustomerService,
    private router: Router
  ) { }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    const customer = this.customerService.getCustomerById(this.id);
    this.customerForm.patchValue({
      id: customer.id,
      companyName: customer.companyName,
      contactName: customer.contactName,
      contactTitle: customer.contactTitle,
      address: customer.address,
      city: customer.city,
      region: customer.region,
      postalCode: customer.postalCode,
      country: customer.country,
      phone: customer.phone,
      fax: customer.fax
    });
  }

  save() {
    const customer = this.customerForm.value;
    customer.id = this.id;
    this.customerService.updateCustomer(customer);
    /*this.customerForm.reset();
    this.router.navigateByUrl('customers');*/
  }
}
