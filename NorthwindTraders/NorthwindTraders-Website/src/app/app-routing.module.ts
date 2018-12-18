import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { WelcomeComponent } from './welcome/welcome.component';
import { CustomerListComponent } from './customers/customer-list/customer-list.component';
import { CustomerDetailsComponent } from './customers/customer-details/customer-details.component';

const routes: Routes = [
  { path: 'customers/:id', component: CustomerDetailsComponent},
  { path: 'customers', component: CustomerListComponent },
  { path: '', component: WelcomeComponent }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
