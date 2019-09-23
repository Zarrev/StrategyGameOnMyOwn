import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-authentication-page',
  templateUrl: './authentication-page.component.html',
  styleUrls: ['./authentication-page.component.scss']
})
export class AuthenticationPageComponent implements OnInit {

  ngOnInit(): void {
    console.log('Authentication Page has loaded!');
  }

}
