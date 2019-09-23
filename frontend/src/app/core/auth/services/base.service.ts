import {Injectable} from '@angular/core';
import {Observable, throwError} from 'rxjs';
import {HttpHeaders} from '@angular/common/http';
import { Helpers } from 'src/app/utils/helper';

@Injectable()
export class BaseService {

  constructor(private helper: Helpers) {
  }

  public extractData(res: Response): any {
    return res.json() || {};
  }

  public handleError(error: Response | any): Observable<never> {
    let errMsg: string;

    if (error instanceof Response) {
      const body = error.json() || '';
      const err = body || JSON.stringify(body);
      errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
    } else {
      errMsg = error.message ? error.message : error.toString();
    }

    console.log(errMsg);

    return throwError(errMsg);
  }

  public header(): { [key: string]: HttpHeaders } {
    let header = new HttpHeaders({'Content-Type': 'application/json'});

    if (Helpers.isAuthenticated()) {
      header = header.append('Authorization', 'Bearer ' + Helpers.getToken());
    }

    return {headers: header};
  }

  public setToken(data: any) {
    this.helper.setToken(data);
  }

  public failToken(error: Response | any): Observable<never> {
    this.helper.failToken();
    return this.handleError(Response);
  }
}
