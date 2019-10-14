import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { User } from '../../models/user';
import { environment } from 'src/environments/environment';
import { BaseService } from '../../../shared/services/base.service';
import { Helpers } from 'src/app/utils/helper';
import * as jwt_decode from 'jwt-decode';
import { ToastService } from 'src/app/shared/services/toast.service';

@Injectable({ providedIn: 'root' })
export class AuthenticationService extends BaseService {

  private currentUserSubject = new BehaviorSubject<User>(null);
  public currentUser: Observable<User>;
  private specAPI = 'account/';
  private helperLogOut: () => void;


  constructor(private http: HttpClient, toastService: ToastService, helper: Helpers) {
    super(helper, toastService);
    this.helperLogOut = () => {
      helper.logout();
      this.currentUserSubject.next(null);
    };
    if (Helpers.isAuthenticated()) {
      const token = Helpers.getToken();
      this.nextUser(token);
    }
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  get user(): Observable<User> {
    return this.currentUserSubject.asObservable();
  }

  logout() {
    this.helperLogOut();
  }

  login(authValues: { Password: string; Username: string; }): Observable<User> {
    const body = JSON.stringify(authValues);
    return this.http.post<any>(environment.apiUrl + this.specAPI + 'login', body, super.header()).pipe(map(token => {
      super.setToken(token);
      return this.nextUser(token.message);
    }),
      catchError(error => super.handleError(error))
    );
  }

  registration(authValues: { Password: string; RepeatedPassword: string; Username: string; CountryName: string; }): Observable<User> {
    const body = JSON.stringify(authValues);
    return this.http.post<any>(environment.apiUrl + this.specAPI + 'register', body, super.header()).pipe(map(token => {
      super.setToken(token);
      return this.nextUser(token.message);
    }),
      catchError(error => super.handleError(error))
    );
  }

  private nextUser(token: any): User {
    const user: User = { username: jwt_decode(token).sub, token};
    this.currentUserSubject.next(user);
    return user;
  }
}
