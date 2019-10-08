import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ArmyPageComponent } from './pages/army-page/army-page.component';


const routes: Routes = [
  {
    path: '',
    component: ArmyPageComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ArmyRoutingModule { }
