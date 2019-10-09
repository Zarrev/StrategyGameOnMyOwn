import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RankPageComponent } from './pages/rank-page/rank-page.component';


const routes: Routes = [
  {
    path: '',
    component: RankPageComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RankTableRoutingModule { }
