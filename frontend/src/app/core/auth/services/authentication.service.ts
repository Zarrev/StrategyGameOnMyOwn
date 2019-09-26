import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { User } from '../models/user';
import { environment } from 'src/environments/environment';
import { BaseService } from './base.service';
import { Helpers } from 'src/app/utils/helper';
import * as jwt_decode from 'jwt-decode';
import { ToastService } from 'src/app/shared/services/toast.service';
import { InternalErrorService } from 'src/app/shared/services/internal-error.service';

@Injectable({ providedIn: 'root' })
export class AuthenticationService extends BaseService {

    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;
    private specAPI = 'account/';
    private helperLogOut: () => void;


    constructor(private http: HttpClient, internalErrorService: InternalErrorService, toastService: ToastService, helper: Helpers) {
        super(helper, toastService, internalErrorService);
        this.helperLogOut = () => {
          helper.logout();
          this.currentUserSubject.next(null);
        };

        if (Helpers.isAuthenticated()) {
          const token = Helpers.getToken();
          const user: User = {username: jwt_decode(token).sub, token};
          this.currentUserSubject = new BehaviorSubject<User>(user);
        } else {
          this.currentUserSubject = new BehaviorSubject<User>(null);
        }
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    logout() {
        this.helperLogOut();
    }

    login(authValues: { Password: string; Username: string; }): Observable<User> {
        const body = JSON.stringify(authValues);
        return this.http.post<any>(environment.apiUrl + this.specAPI + 'login', body, super.header()).pipe(map(token => {
            super.setToken(token);
            const user: User = {username: authValues.Username, token};
            this.currentUserSubject.next(user);
            return user;
        }),
          catchError(error => super.handleError(error))
        );
      }

      registration(authValues: { Password: string; RepeatedPassword: string; Username: string; CountryName: string; }): Observable<User> {
        const body = JSON.stringify(authValues);
        return this.http.post<any>(environment.apiUrl + this.specAPI + 'register', body, super.header()).pipe(map(token => {
            super.setToken(token);
            const user: User = {username: authValues.Username, token};
            this.currentUserSubject.next(user);
            return user;
        }),
          catchError(error => super.handleError(error))
        );
      }
}
