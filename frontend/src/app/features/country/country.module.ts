import { NgModule } from '@angular/core';

import { CountryRoutingModule } from './country-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { MainPageComponent } from './pages/main-page/main-page.component';


@NgModule({
  declarations: [MainPageComponent],
  imports: [
    SharedModule,
    CountryRoutingModule
  ]
})
export class CountryModule { }
