import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription, Observable } from 'rxjs';
import { BuildingEnum } from 'src/app/features/country/models/building-enum';
import { CountryService } from 'src/app/features/country/services/country.service';
import { Router } from '@angular/router';
import { getDevelopmentEnum } from 'src/app/features/country/models/development-enum';

@Component({
  selector: 'app-development',
  templateUrl: './development.component.html',
  styleUrls: ['./development.component.scss']
})
export class DevelopmentComponent implements OnInit, OnDestroy {

  private _developments: {
    mudTractor: boolean, sludgeharvester: boolean, coralWall: boolean,
    sonarGun: boolean, underwaterMaterialArts: boolean, alchemy: boolean
  };
  private subsc: Subscription[] = [];
  selected: BuildingEnum = -1;
  name = -1;
  remaining = 0;

  constructor(private countryService: CountryService, private router: Router) {
    this.init();
  }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    this.subsc.forEach(x => x.unsubscribe());
  }

  private init(): void {
    this.subsc.push(this.countryService.getDevelopments().subscribe(developments => {
      this._developments = developments;
    }));
    this.subsc.push(this.countryService.getDevRound().subscribe(value => this.remaining = value));
    this.subsc.push(this.countryService.getDevelopingName().subscribe(value => this.name = value));
  }

  get developments(): {
    mudTractor: boolean, sludgeharvester: boolean, coralWall: boolean,
    sonarGun: boolean, underwaterMaterialArts: boolean, alchemy: boolean
  } {
    return this._developments;
  }

  buy(): void {
    this.subsc.push(this.countryService.postDevelopAction(this.selected).subscribe(() => console.log('Bought!')));
    this.cancel();

  }

  cancel(): void {
    this.router.navigate(['/']);
  }

  setSelected(eventNumber: number): void {
    if (!this._developments[getDevelopmentEnum(eventNumber)]) {
      this.selected = eventNumber;
    }
  }
}
