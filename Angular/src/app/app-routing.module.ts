import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ConsultDetailsComponent } from './consult-details/consult-details.component';
import { BonusDetailsComponent } from './bonus-details/bonus-details.component';
import { BonusDetailComponent } from './bonus-details/bonus-detail/bonus-detail.component';
import { BonusDetailListComponent } from './bonus-details/bonus-detail-list/bonus-detail-list.component';
import { ConsultDetailComponent } from './consult-details/consult-detail/consult-detail.component';
import { ConsultDetailListComponent } from './consult-details/consult-detail-list/consult-detail-list.component';

const routes: Routes = [
  { path: '', redirectTo: 'consults', pathMatch: 'full' },
  { path: 'consults/:id', component: ConsultDetailsComponent,
    children:[
      { path: '', redirectTo: 'consult', pathMatch: 'full' },
      { path: 'consult', component: ConsultDetailComponent },
      { path: 'consult-list', component: ConsultDetailListComponent }
    ]
  },
  { path: 'bonus', loadChildren: './bonus-details/bonus-details.module#BonusDetailsModule', }
  /* { path: 'bonus', component: BonusDetailsComponent, */
 /*   children:[
    { path: '', component: BonusDetailListComponent },
    { path: 'bonus-list', component: BonusDetailListComponent },
    { path: ':id', component: BonusDetailComponent }
    ], */
 /* }*/
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
export const routingComponents = [ConsultDetailsComponent,  BonusDetailsComponent]
