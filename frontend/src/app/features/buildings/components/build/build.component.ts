import { Component, OnInit, OnDestroy } from '@angular/core';
import { CountryService } from 'src/app/features/country/services/country.service';
import { Subscription, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { BuildingPagesComponent } from '../../pages/building-pages/building-pages.component';
import { BuildingEnum } from 'src/app/features/country/models/building-enum';

@Component({
  selector: 'app-build',
  templateUrl: './build.component.html',
  styleUrls: ['./build.component.scss']
})
export class BuildComponent implements OnInit, OnDestroy {

  private _reefcastleNumber: number;
  private _flowcontrollerNumber: number;
  private subsc: Subscription[] = [];
  name = -1;
  remaining = 0;
  selected: BuildingEnum = -1;

  constructor(private countryService: CountryService, private router: Router) {
    this.init();
  }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    this.subsc.forEach(x => x.unsubscribe());
  }

  private init(): void {
    this.subsc.push(this.countryService.getBuildings().subscribe(buildings => {
      this._flowcontrollerNumber = buildings.flowControllNumber;
      this._reefcastleNumber = buildings.reefCastleNumber;
    }));
    this.subsc.push(this.countryService.getBuildRound().subscribe(value => this.remaining = value));
    this.subsc.push(this.countryService.getBuildingName().subscribe(value => this.name = value));
  }

  get reefcastleNumber(): number {
    return this._reefcastleNumber;
  }

  get flowcontrollerNumber(): number {
    return this._flowcontrollerNumber;
  }

  buy(): void {
    this.subsc.push(this.countryService.postBuildAction(this.selected).subscribe(() => console.log('Bought!')));
    this.cancel();

  }

  cancel(): void {
    this.router.navigate(['/']);
  }

  setSelected(eventNumber: number): void {
    this.selected = eventNumber;
  }
}
