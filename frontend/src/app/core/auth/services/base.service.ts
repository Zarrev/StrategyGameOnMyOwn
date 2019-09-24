import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { Helpers } from 'src/app/utils/helper';
import { ToastService } from 'src/app/shared/services/toast.service';

@Injectable()
export class BaseService {

  constructor(private helper: Helpers, private toastService: ToastService) {
  }

  public extractData(res: Response): any {
    return res.json() || {};
  }

  public handleError(error: Response | any): Observable<never> {
    console.log(error.status);
    console.log(error.statusText);
    let errorList = '';
    error.error.error.forEach(element => {
      errorList = errorList + element + '\n';
    });
    this.toastService.setError(error.status + ' ' + error.statusText, errorList);
    return throwError(errorList);
  }

  public header(): { [key: string]: HttpHeaders } {
    let header = new HttpHeaders({ 'Content-Type': 'application/json' });

    if (Helpers.isAuthenticated()) {
      header = header.append('Authorization', 'Bearer ' + Helpers.getToken());
    }

    return { headers: header };
  }

  public setToken(data: any) {
    this.helper.setToken(data);
  }

  public failToken(error: Response | any): Observable<never> {
    this.helper.failToken();
    return this.handleError(Response);
  }
}
