import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class InternalErrorService {

  private html = new BehaviorSubject<any>('');

  constructor(private router: Router) { }

  set htmlTemplate(html) {
    if (html !== null && html !== undefined) {
      this.html.next(html);
      this.router.navigate(['error']);
    }
  }

  get htmlTemplate(): Observable<any> {
    return this.html.asObservable();
  }
}
