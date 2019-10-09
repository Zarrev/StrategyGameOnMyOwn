import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RankTableRoutingModule } from './rank-table-routing.module';
import { RankPageComponent } from './pages/rank-page/rank-page.component';
import { RankTableComponent } from './components/rank-table/rank-table.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [RankPageComponent, RankTableComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RankTableRoutingModule
  ]
})
export class RankTableModule { }
