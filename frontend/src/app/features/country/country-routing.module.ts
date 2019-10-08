import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainPageComponent } from './pages/main-page/main-page.component';

const routes: Routes = [
  {
    path: '',
    component: MainPageComponent,
    children: [
      {
        path: 'build',
        loadChildren: './../buildings/buildings.module#BuildingsModule'
      },
      {
        path: 'develop',
        loadChildren: './../develop/develop.module#DevelopModule'
      },
      {
        path: 'army',
        loadChildren: './../army/army.module#ArmyModule'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CountryRoutingModule { }
