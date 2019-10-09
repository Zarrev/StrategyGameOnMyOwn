import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/core/models/user';

@Component({
  selector: 'app-attack-page',
  templateUrl: './attack-page.component.html',
  styleUrls: ['./attack-page.component.scss']
})
export class AttackPageComponent implements OnInit {

  private selectedUser: User;

  constructor() { }

  ngOnInit() {
  }

  onSelected(user: User) {
    this.selectedUser = user;
    console.log(user);
  }
}
