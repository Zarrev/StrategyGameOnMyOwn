import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/core/models/user';
import { SelectedArmy } from '../../models/selected-army.interface';
import { Router } from '@angular/router';
import { MercenaryRequest } from 'src/app/features/country/models/mercenary-request';
import { BattleService } from 'src/app/features/battle/services/battle.service';
import { Subscription } from 'rxjs';
import { CountryService } from 'src/app/features/country/services/country.service';


@Component({
  selector: 'app-attack-page',
  templateUrl: './attack-page.component.html',
  styleUrls: ['./attack-page.component.scss']
})
export class AttackPageComponent implements OnInit {

  private selectedUser: User = null;
  private selectedArmy: SelectedArmy = null;
  private subsc: Subscription[] = [];

  constructor(private router: Router, private battleService: BattleService, private countryService: CountryService) { }

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
    this.subsc.push(this.battleService.postBattleAction(mercenaryRequest, this.selectedUser).subscribe(resp => console.log(resp)));
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
