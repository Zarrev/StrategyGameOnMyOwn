import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { Helpers } from 'src/app/utils/helper';
import { ToastService } from 'src/app/shared/services/toast.service';
import { InternalErrorService } from 'src/app/shared/services/internal-error.service';

@Injectable()
export class BaseService {

  constructor(private helper: Helpers, private toastService: ToastService, private internalErrorService: InternalErrorService) {
  }

  public extractData(res: Response): any {
    return res.json() || {};
  }

  public handleError(error: Response | any): Observable<never> {
    if (error.status === 500) {
      this.internalErrorService.htmlTemplate = error.error;
      return throwError(error.message);
    }
    const errorList = [];
    error.error.error.forEach((element: string) => {
      errorList.push(element);
    });
    this.toastService.setError({errorCode: error.status + ' ' + error.statusText + ':', errorMessages: errorList});
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
