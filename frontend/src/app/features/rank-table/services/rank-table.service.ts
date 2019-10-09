import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/shared/services/base.service';
import { ToastService } from 'src/app/shared/services/toast.service';
import { Helpers } from 'src/app/utils/helper';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { map, catchError } from 'rxjs/operators';
import { User } from 'src/app/core/models/user';

@Injectable({
  providedIn: 'root'
})
export class RankTableService extends BaseService {

  private specAPI = 'account/';
  private userList = new BehaviorSubject<User[]>([]);
  private searchUserList = new BehaviorSubject<User[]>([]);

  constructor(private http: HttpClient, toastService: ToastService, helper: Helpers) {
    super(helper, toastService);
  }

  get users(): Observable<User[]> {
    return this.userList.asObservable();
  }

  get searchUsers(): Observable<User[]> {
    return this.searchUserList.asObservable();
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(environment.apiUrl + this.specAPI + 'getusers', super.header()).pipe(
      map(users => {
        this.userList.next(users);
        return users;
      }),
      catchError(error => super.handleError(error))
    );
  }
  
  GetUsersBySearchWithPoints(searchText: string): Observable<User[]> {
    return this.http.get<User[]>(environment.apiUrl + this.specAPI + 'getusersbysearch/' + searchText, super.header()).pipe(
      map(users => {
        this.searchUserList.next(users);
        return users;
      }),
      catchError(error => super.handleError(error))
    );
  }

}
