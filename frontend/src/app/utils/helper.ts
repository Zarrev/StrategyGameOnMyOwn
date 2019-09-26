import {Injectable} from '@angular/core';
import {Observable, Subject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Helpers {

  public static tokenKey = 'token';
  private authenticationChanged = new Subject<boolean>();

  constructor() {
  }

  public static isAuthenticated(): boolean {
    const token = window.localStorage.getItem(Helpers.tokenKey);
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

    return JSON.parse(window.localStorage.getItem(Helpers.tokenKey)).message;
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
    window.localStorage.setItem(Helpers.tokenKey, value);
    this.authenticationChanged.next(Helpers.isAuthenticated());
  }
}
