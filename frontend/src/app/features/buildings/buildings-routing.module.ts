import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BuildingPagesComponent } from './pages/building-pages/building-pages.component';


const routes: Routes = [
  {
    path: '',
    component: BuildingPagesComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BuildingsRoutingModule { }
