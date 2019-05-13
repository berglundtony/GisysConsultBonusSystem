import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'
import { AppRoutingModule, routingComponents } from './app-routing.module';
import { AppComponent } from './app.component';
import { ConsultDetailComponent } from './consult-details/consult-detail/consult-detail.component';
import { ConsultDetailListComponent } from './consult-details/consult-detail-list/consult-detail-list.component';
import { BonusDetailsComponent } from './bonus-details/bonus-details.component';
import { ConsultDetailService } from './shared/consult-detail.service';
import { HttpClientModule } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { BonusDetailService } from './shared/bonus-detail.service';
import { DataTablesModule } from 'angular-datatables';
import { ModalModule } from 'ngx-bootstrap/modal';
/*import { BonusDetailComponent } from './bonus-details/bonus-detail/bonus-detail.component';
import { BonusDetailListComponent } from './bonus-details/bonus-detail-list/bonus-detail-list.component'; */




@NgModule({
  declarations: [
    AppComponent,
    routingComponents,
    ConsultDetailComponent,
    ConsultDetailListComponent,
    BonusDetailsComponent
   /* BonusDetailComponent,
    BonusDetailListComponent */
   
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    DataTablesModule,
    ModalModule.forRoot()
  ], 
  providers: [ConsultDetailService, BonusDetailService, DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }

