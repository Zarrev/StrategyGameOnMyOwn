import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { User } from '../models/user';
import { environment } from 'src/environments/environment';
import { BaseService } from './base.service';
import { Helpers } from 'src/app/utils/helper';

@Injectable({ providedIn: 'root' })
export class AuthenticationService extends BaseService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;
    private specAPI = '/token';


    constructor(private http: HttpClient, helper: Helpers) {
        super(helper);
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem(environment.localStorageTokenKey)));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem(environment.localStorageTokenKey);
        this.currentUserSubject.next(null);
    }

    login(authValues: { password: string; username: string }): Observable<User> {
        const body = JSON.stringify(authValues);
        return this.http.post<any>(environment.apiUrl + this.specAPI, body, super.header()).pipe(map(token => {
            super.setToken(token);
            const user: User = {username: authValues.username, token};
            this.currentUserSubject.next(user);
            return user;
        }),
          catchError(error => super.handleError(error))
        );
      }
}
