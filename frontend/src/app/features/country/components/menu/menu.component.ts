import { Component, OnInit, OnDestroy } from '@angular/core';
import { CountryService } from '../../services/country.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit, OnDestroy {
  
  private subsc: Subscription[] = [];

  constructor(private countryService: CountryService) { }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    this.subsc.forEach(x => x.unsubscribe());
  }

  next() {
    this.subsc.push(this.countryService.next().subscribe(resp => console.log(resp)));
    this.subsc.push(this.countryService.getMercenaries().subscribe());
  }

}
