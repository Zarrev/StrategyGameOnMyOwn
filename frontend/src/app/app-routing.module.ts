import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuardGuard } from './core/guards/auth-guard.guard';


const routes: Routes = [
  {
    path: '',
    loadChildren: './core/core.module#CoreModule'
  },
  {
    path: 'country',
    loadChildren: './features/country/country.module#CountryModule',
    canActivate: [AuthGuardGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
