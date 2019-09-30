import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BaseService } from 'src/app/core/auth/services/base.service';
import { ToastService } from 'src/app/shared/services/toast.service';
import { Helpers } from 'src/app/utils/helper';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CountryService extends BaseService {

  private specAPI = 'countries/byuser';

  constructor(private http: HttpClient, toastService: ToastService, helper: Helpers) {
    super(helper, toastService);
  }

  getCountryByUserId(userId: string): Observable<Country> {
    return this.http.get<Country>(environment.apiUrl + this.specAPI + '/' + userId, super.header()).pipe(
      map(country => {
        return country;
      }),
      catchError(error => super.handleError(error))
    );

  }
}
