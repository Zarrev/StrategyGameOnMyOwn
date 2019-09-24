import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/core/auth/services/authentication.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent implements OnInit {

  constructor(private router: Router, private authenticationService: AuthenticationService) {  }

  ngOnInit() {
  }

  logOut() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }

}
