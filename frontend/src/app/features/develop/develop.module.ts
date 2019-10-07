import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DevelopRoutingModule } from './develop-routing.module';
import { DevPagesComponent } from './pages/dev-pages/dev-pages.component';
import { DevelopmentComponent } from './components/development/development.component';


@NgModule({
  declarations: [DevPagesComponent, DevelopmentComponent],
  imports: [
    CommonModule,
    DevelopRoutingModule
  ]
})
export class DevelopModule { }
