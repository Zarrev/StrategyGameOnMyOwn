import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { Subscription, Observable } from 'rxjs';
import { debounceTime, switchMap } from 'rxjs/operators';
import { RankTableService } from '../../services/rank-table.service';
import { User } from 'src/app/core/models/user';
import { FormControl } from '@angular/forms';
import { AuthenticationService } from 'src/app/core/auth/services/authentication.service';


@Component({
  selector: 'app-rank-table',
  templateUrl: './rank-table.component.html',
  styleUrls: ['./rank-table.component.scss']
})
export class RankTableComponent implements OnInit, OnDestroy {

  @Input() fullTable: boolean = true;
  @Input() selectable: boolean = false;
  @Input() meOnList: boolean = true;
  @Output() selected = new EventEmitter<User>();
  selectedUser: User;
  private subsc: Subscription[] = [];
  private userList: User[] = [];
  queryField: FormControl = new FormControl();

  constructor(private rankTableService: RankTableService, private authService: AuthenticationService) {
    this.subsc.push(this.rankTableService.getUsers().subscribe(users => {
      this.userList = users.sort((a, b) => a.point > b.point ? -1 : 1);
    }));
  }

  get me(): Observable<User> {
    return this.authService.currentUser;
  }

  get users(): User[] {
    return this.userList;
  }

  ngOnInit() {
    this.subsc.push(this.queryField.valueChanges
      .pipe(
        debounceTime(200),
        switchMap(searchText => {
          if (searchText !== undefined && searchText !== null && searchText !== '') {
            return this.rankTableService.GetUsersBySearchWithPoints(searchText);
          }
          return this.rankTableService.getUsers();
        })
      ).subscribe(users => {
        this.userList = users.sort((a, b) => a.point > b.point ? -1 : 1);
      })
    );
  }

  ngOnDestroy() {
    this.subsc.forEach(x => x.unsubscribe());
  }

  onClick(selectedUser: User) {
    if (this.selectable) {
      this.selected.emit(selectedUser);
      this.selectedUser = selectedUser;
    }
  }

}
