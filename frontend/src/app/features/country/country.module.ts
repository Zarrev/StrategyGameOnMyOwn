import { NgModule } from '@angular/core';

import { CountryRoutingModule } from './country-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { HeaderComponent } from './components/header/header.component';
import {NgbModule, NgbTooltipModule} from '@ng-bootstrap/ng-bootstrap';
import { UserPartComponent } from './components/user-part/user-part.component';
import { MenuComponent } from './components/menu/menu.component';


@NgModule({
  declarations: [MainPageComponent, HeaderComponent, UserPartComponent, MenuComponent],
  imports: [
    SharedModule,
    CountryRoutingModule,
    NgbModule,
    NgbTooltipModule
  ]
})
export class CountryModule { }
