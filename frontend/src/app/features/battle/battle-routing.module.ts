import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BattlePageComponent } from './pages/battle-page/battle-page.component';


const routes: Routes = [
  {
    path: '',
    component: BattlePageComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BattleRoutingModule { }
