import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ArmyRoutingModule } from './army-routing.module';
import { ArmyPageComponent } from './pages/army-page/army-page.component';
import { ArmyComponent } from './components/army/army.component';


@NgModule({
  declarations: [ArmyPageComponent, ArmyComponent],
  imports: [
    CommonModule,
    ArmyRoutingModule
  ]
})
export class ArmyModule { }
