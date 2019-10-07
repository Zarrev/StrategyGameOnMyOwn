import { Component, OnInit, OnDestroy } from '@angular/core';
import { User } from 'src/app/core/models/user';
import { Subscription, Observable } from 'rxjs';
import { AuthenticationService } from 'src/app/core/auth/services/authentication.service';
import { CountryService } from '../../services/country.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, OnDestroy {

  user: User;
  _country: Country;
  private subscription: Subscription[] = [];

  constructor(private authenticationService: AuthenticationService, private countryService: CountryService) {
    this.subscription.push(this.authenticationService.currentUser.subscribe(user => {
      this.user = user;
      this.subscription.push(this.countryService.getUserCountry().subscribe(country => console.log('Country has got!')));
      this.subscription.push(this.countryService.country.subscribe(country => this._country = country));
    }));
    this.subscription.push(this.countryService.getCurrentRound().subscribe(round => console.log('Round has stepped!')));
    this.subscription.push(this.countryService.getRank().subscribe(rank => console.log('Your rank is ' + rank)));
  }

  ngOnInit() {
  }

  get round(): Observable<number> {
    return this.countryService.round;
  }

  get rank(): Observable<number> {
    return this.countryService.rank;
  }

  get country(): Country {
    return this._country;
  }

  ngOnDestroy(): void {
    this.subscription.forEach(x => x.unsubscribe());
  }

}
