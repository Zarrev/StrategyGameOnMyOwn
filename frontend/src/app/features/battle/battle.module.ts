import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BattleRoutingModule } from './battle-routing.module';
import { BattlePageComponent } from './pages/battle-page/battle-page.component';
import { BattleComponent } from './components/battle/battle.component';


@NgModule({
  declarations: [BattlePageComponent, BattleComponent],
  imports: [
    CommonModule,
    BattleRoutingModule
  ]
})
export class BattleModule { }
