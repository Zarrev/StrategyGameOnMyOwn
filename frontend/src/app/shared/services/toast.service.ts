import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  private _error = new Subject<string>();

  constructor() { }

  public setError(errorCode: string, errorMessage: string) {
    this._error.next(errorCode + ': ' + errorMessage);
  }

  get error() {
    return this._error.asObservable();
  }
}
