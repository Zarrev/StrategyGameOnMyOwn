import { Component, OnInit, OnDestroy } from '@angular/core';
import { CountryService } from 'src/app/features/country/services/country.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { MercenaryRequest } from 'src/app/features/country/models/mercenary-request';

@Component({
  selector: 'app-army',
  templateUrl: './army.component.html',
  styleUrls: ['./army.component.scss']
})
export class ArmyComponent implements OnInit, OnDestroy {

  laserSharkNumber = 0;
  selectedLaserSharkNumber = 0;
  assaultSeaDogNumber = 0;
  selectedAssaultSeaDogNumber = 0;
  battleSeahorseNumber = 0;
  selectedBattleSeahorseNumber = 0;
  private subsc: Subscription[] = [];

  constructor(private countryService: CountryService, private router: Router) {
    this.init();
  }

  ngOnInit() {
  }

  ngOnDestroy() {
    this.subsc.forEach(x => x.unsubscribe());
  }
  private init(): void {
    this.subsc.push(this.countryService.getMercenaries().subscribe(values => {
      this.laserSharkNumber = values.laserShark;
      this.assaultSeaDogNumber = values.seaDogNumber;
      this.battleSeahorseNumber = values.battleSeahorse;
    }));
  }

  change(type: string, operation: '+' | '-') {
    if ('+' === operation) {
      this.increase(type);
    } else if ('-' === operation) {
      this.decrease(type);
    }
  }

  private decrease(type: string): void {
    switch (type) {
      case 'laserShark': this.selectedLaserSharkNumber > 0 ? this.selectedLaserSharkNumber-- : this.selectedLaserSharkNumber = 0; break;
      case 'assaultSeaDog':
        this.selectedAssaultSeaDogNumber > 0 ? this.selectedAssaultSeaDogNumber-- : this.selectedAssaultSeaDogNumber = 0; break;
      case 'battleSeahorse':
        this.selectedBattleSeahorseNumber > 0 ? this.selectedBattleSeahorseNumber-- : this.selectedBattleSeahorseNumber = 0; break;
    }
  }

  private increase(type: string): void {
    switch (type) {
      case 'laserShark': this.selectedLaserSharkNumber++; break;
      case 'assaultSeaDog': this.selectedAssaultSeaDogNumber++; break;
      case 'battleSeahorse': this.selectedBattleSeahorseNumber++; break;
    }
  }

  buy(): void {
    const mercenaries: MercenaryRequest = {
      AssaultSeaDog: this.selectedAssaultSeaDogNumber,
      BattleSeahorse: this.selectedBattleSeahorseNumber,
      LaserShark: this.selectedLaserSharkNumber
    };
    this.subsc.push(this.countryService.postHireAction(mercenaries).subscribe(() => console.log('Bought!')));
    this.cancel();

  }

  cancel(): void {
    this.subsc.push(this.countryService.getMercenaries().subscribe());
    this.router.navigate(['/']);
  }
}
