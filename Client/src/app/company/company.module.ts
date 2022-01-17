import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
// import { ProfileComponent } from './profile/profile.component';
import { SharedModule } from '../shared/shared.module';
import { CompanyRoutingModule } from './company-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { CompanyService } from './company.service';



@NgModule({
//   declarations: [ProfileComponent],
providers: [
    CompanyService
],
  imports: [
    CommonModule,
    SharedModule,
    CompanyRoutingModule,
    ReactiveFormsModule,
  ],
//   exports: [ProfileComponent]
})
export class CompanyModule { }
