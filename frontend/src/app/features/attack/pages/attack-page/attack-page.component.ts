import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/core/models/user';
import { SelectedArmy } from '../../models/selected-army.interface';
import { Router } from '@angular/router';
import { MercenaryRequest } from 'src/app/features/country/models/mercenary-request';
import { BattleService } from 'src/app/features/battle/services/battle.service';


@Component({
  selector: 'app-attack-page',
  templateUrl: './attack-page.component.html',
  styleUrls: ['./attack-page.component.scss']
})
export class AttackPageComponent implements OnInit {

  private selectedUser: User = null;
  private selectedArmy: SelectedArmy = null;

  constructor(private router: Router, private battleService: BattleService) { }

  ngOnInit() {
  }

  onSelected(user: User) {
    this.selectedUser = user;
  }

  onSelectArmy(army: SelectedArmy) {
    this.selectedArmy = army;
  }

  start() {
    const mercenaryRequest: MercenaryRequest = {
      AssaultSeaDog: this.selectedArmy.assaultSeaDog,
      BattleSeahorse: this.selectedArmy.battleSeahorse,
      LaserShark: this.selectedArmy.laserShark
    };
    this.battleService.postBattleAction(mercenaryRequest, this.selectedUser);
    this.cancel();
  }

  cancel(): void {
    this.router.navigate(['/']);
  }

  get enemy() {
    return this.selectedUser;
  }

  get army() {
    return this.selectedArmy;
  }
}
