import { Component, OnInit, OnDestroy, Output, EventEmitter } from '@angular/core';
import { CountryService } from 'src/app/features/country/services/country.service';
import { Subscription } from 'rxjs';
import { SelectedArmy } from '../../models/selected-army.interface';

@Component({
  selector: 'app-attack',
  templateUrl: './attack.component.html',
  styleUrls: ['./attack.component.scss']
})
export class AttackComponent implements OnInit, OnDestroy {

  maxRanges = { laserShark: 0, assaultSeaDog: 0, battleSeahorse: 0 };
  values: SelectedArmy = { laserShark: 0, assaultSeaDog: 0, battleSeahorse: 0 };
  private subsc: Subscription[] = [];
  @Output() finalOutput = new EventEmitter<SelectedArmy>();

  constructor(private countryService: CountryService) {
    this.countryService.getMercenaries().subscribe(values => {
      this.maxRanges = {laserShark: values.laserShark, assaultSeaDog: values.seaDogNumber, battleSeahorse: values.battleSeahorse};
    });
  }

  set(property: string, value: number) {
    this.values[property] = value;
  }

  send() {
    this.finalOutput.emit(this.values);
  }

  ngOnInit() {
  }

  ngOnDestroy() {
    this.subsc.forEach(x => x.unsubscribe());
  }
}
