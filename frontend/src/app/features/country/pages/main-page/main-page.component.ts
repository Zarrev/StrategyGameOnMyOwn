import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/core/auth/services/authentication.service';
import { User } from 'src/app/core/models/user';
import { Subscription } from 'rxjs';
import { CountryService } from '../../services/country.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent implements OnInit, OnDestroy {

  user: User;
  country: Country;
  private subscription: Subscription[] = [];

  constructor(private router: Router, private authenticationService: AuthenticationService, private countryService: CountryService) { }

  ngOnInit() {
    this.subscription.push(this.authenticationService.currentUser.subscribe(user => {
      this.user = user;
      this.subscription.push(this.countryService.getUserCountry().subscribe(country => this.country = country));
    }));
  }

  logOut() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }

  ngOnDestroy(): void {
    this.subscription.forEach(x => x.unsubscribe());
  }
}
