import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  private _error = new Subject<{errorCode: string; errorMessages: string[];}>();

  constructor() { }

  public setError(errorContent: {errorCode: string; errorMessages: string[];}) {
    this._error.next(errorContent);
  }

  get error() {
    return this._error.asObservable();
  }
}
