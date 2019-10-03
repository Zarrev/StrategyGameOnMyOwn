import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/core/auth/services/authentication.service';

@Component({
  selector: 'app-user-part',
  templateUrl: './user-part.component.html',
  styleUrls: ['./user-part.component.scss']
})
export class UserPartComponent implements OnInit {

  name: string;

  constructor(private router: Router, private authenticationService: AuthenticationService) {
    this.name = this.authenticationService.currentUserValue.username;
  }

  ngOnInit() {
  }

  logOut() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }
}
