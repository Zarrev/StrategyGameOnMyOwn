import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DevPagesComponent } from './pages/dev-pages/dev-pages.component';


const routes: Routes = [
  {
    path: '',
    component: DevPagesComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DevelopRoutingModule { }
