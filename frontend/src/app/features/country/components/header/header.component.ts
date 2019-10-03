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
  country: Country;
  globalRound: number;
  private subscription: Subscription[] = [];

  constructor(private authenticationService: AuthenticationService, private countryService: CountryService) {
    this.subscription.push(this.authenticationService.currentUser.subscribe(user => {
      this.user = user;
      this.subscription.push(this.countryService.getUserCountry().subscribe(country => this.country = country));
    }));
    this.countryService.getCurrentRound();
  }

  ngOnInit() {
  }

  get round(): Observable<number> {
    return this.countryService.round;
  }

  get rank(): Observable<number> {
    return this.countryService.rank;
  }

  ngOnDestroy(): void {
    this.subscription.forEach(x => x.unsubscribe());
  }

}
