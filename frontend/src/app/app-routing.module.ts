import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuardGuard } from './core/guards/auth-guard.guard';
import { InternalErrorComponent } from './shared/components/internal-error/internal-error.component';


const routes: Routes = [
  {
    path: '',
    loadChildren: './core/core.module#CoreModule'
  },
  {
    path: 'country',
    loadChildren: './features/country/country.module#CountryModule',
    canActivate: [AuthGuardGuard]
  },
  {
    path: 'error',
    component: InternalErrorComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
