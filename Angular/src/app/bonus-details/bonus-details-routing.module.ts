import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BonusDetailsComponent } from './bonus-details.component';
import { BonusDetailComponent } from './bonus-detail/bonus-detail.component';
import { BonusDetailListComponent } from './bonus-detail-list/bonus-detail-list.component';

const routes: Routes = [
 { path: '', component:BonusDetailListComponent } ,
 { path: 'bonus-list', component: BonusDetailListComponent },
 { path: ':id', component: BonusDetailComponent }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BonusDetailsRoutingModule { }
