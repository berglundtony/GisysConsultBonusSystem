import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BonusDetailsRoutingModule } from './bonus-details-routing.module';
import { BonusDetailComponent } from './bonus-detail/bonus-detail.component';
import { BonusDetailListComponent } from './bonus-detail-list/bonus-detail-list.component'; 
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { DataTablesModule } from 'angular-datatables';

@NgModule({
  declarations: [BonusDetailComponent,
    BonusDetailListComponent],
  imports: [
    CommonModule,
    BonusDetailsRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    DataTablesModule
  ]
})
export class BonusDetailsModule { }
