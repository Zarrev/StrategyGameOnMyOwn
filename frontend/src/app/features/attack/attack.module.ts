import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AttackRoutingModule } from './attack-routing.module';
import { AttackPageComponent } from './pages/attack-page/attack-page.component';
import { AttackComponent } from './components/attack/attack.component';
import { RankTableModule } from '../rank-table/rank-table.module';
import { BattleModule } from '../battle/battle.module';


@NgModule({
  declarations: [AttackPageComponent, AttackComponent],
  imports: [
    CommonModule,
    AttackRoutingModule,
    RankTableModule,
    BattleModule
  ]
})
export class AttackModule { }
