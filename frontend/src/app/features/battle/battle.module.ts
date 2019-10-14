import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BattleRoutingModule } from './battle-routing.module';
import { BattlePageComponent } from './pages/battle-page/battle-page.component';
import { BattleComponent } from './components/battle/battle.component';
import { BattleService } from './services/battle.service';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [BattlePageComponent, BattleComponent],
  imports: [
    CommonModule,
    BattleRoutingModule,
    HttpClientModule
  ],
  providers: [
    BattleService
  ]
})
export class BattleModule { }
