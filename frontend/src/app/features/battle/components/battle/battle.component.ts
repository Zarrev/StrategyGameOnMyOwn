import { Component, OnInit, OnDestroy } from '@angular/core';
import { BattleService } from '../../services/battle.service';
import { Subscription, Observable } from 'rxjs';
import { Battle } from 'src/app/features/battle/models/battle';

@Component({
  selector: 'app-battle',
  templateUrl: './battle.component.html',
  styleUrls: ['./battle.component.scss'],
  providers: [BattleService]
})
export class BattleComponent implements OnInit, OnDestroy {

  private subsc: Subscription[] = [];
  private _battles: Battle[] = [];

  constructor(private battleService: BattleService) {
    this.subsc.push(this.battleService.getBattles().subscribe(battles => {
      this._battles = battles;
    }));
  }

  get battles(): Battle[] {
    return this._battles;
  }

  ngOnInit() {
  }

  ngOnDestroy() {
    this.subsc.forEach(x => x.unsubscribe());
  }
}
