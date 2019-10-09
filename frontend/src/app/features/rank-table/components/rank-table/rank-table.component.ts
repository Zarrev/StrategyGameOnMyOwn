import { Component, OnInit, OnDestroy } from '@angular/core';
import { fromEvent, Subscription } from 'rxjs';
import { ajax } from 'rxjs/ajax';
import { map, filter, debounceTime, distinctUntilChanged, switchMap, switchMapTo } from 'rxjs/operators';
import { RankTableService } from '../../services/rank-table.service';
import { User } from 'src/app/core/models/user';
import { FormControl } from '@angular/forms';


@Component({
  selector: 'app-rank-table',
  templateUrl: './rank-table.component.html',
  styleUrls: ['./rank-table.component.scss']
})
export class RankTableComponent implements OnInit, OnDestroy {

  private subsc: Subscription[] = [];
  private userList: User[] = [];
  queryField: FormControl = new FormControl();

  constructor(private rankTableService: RankTableService) {
    this.subsc.push(this.rankTableService.getUsers().subscribe(users => {
      this.userList = users.sort((a, b) => a.point > b.point ? -1 : 1);
      console.log(this.userList);
      console.log(users);
    }));
  }

  get users(): User[] {
    return this.userList;
  }

  ngOnInit() {
    this.subsc.push(this.queryField.valueChanges
      .pipe(
        debounceTime(200),
        switchMap(searchText => {
          if (searchText !== undefined && searchText !== null && searchText !== ''){
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

}
