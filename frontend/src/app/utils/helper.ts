import {Injectable} from '@angular/core';
import {Observable, Subject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Helpers {

  private authenticationChanged = new Subject<boolean>();

  constructor() {
  }

  public static isAuthenticated(): boolean {
    const token = window.localStorage.getItem('token');
    return (
      token !== undefined &&
      token !== null &&
      token !== 'null' &&
      token !== 'undefined' &&
      token !== '');
  }

  public static getToken(): any {
    if (!Helpers.isAuthenticated()) {
      return '';
    }

    return JSON.parse(window.localStorage.getItem('token')).token;
  }

  public isAuthenticationChanged(): Observable<boolean> {
    return this.authenticationChanged.asObservable();
  }

  public setToken(data: any) {
    this.setStorageToken(JSON.stringify(data));
  }

  public failToken(): void {
    this.setStorageToken(undefined);
  }

  public logout(): void {
    this.setStorageToken(undefined);
  }

  private setStorageToken(value: any): void {
    window.localStorage.setItem('token', value);
    this.authenticationChanged.next(Helpers.isAuthenticated());
  }
}
