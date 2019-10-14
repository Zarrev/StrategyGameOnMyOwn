import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BaseService } from 'src/app/shared/services/base.service';
import { ToastService } from 'src/app/shared/services/toast.service';
import { Helpers } from 'src/app/utils/helper';
import { map, catchError } from 'rxjs/operators';
import { MercenaryRequest } from '../models/mercenary-request';
import { User } from 'src/app/core/models/user';
import { Battle } from '../../battle/models/battle';

@Injectable({
  providedIn: 'root'
})
export class CountryService extends BaseService {

  private specAPI = 'countries/mycountry';
  private _round = new BehaviorSubject<number>(0);
  private _rank = new BehaviorSubject<number>(0);
  private _country = new BehaviorSubject<Country>(null);
  private _mercenaries = new BehaviorSubject<{ seaDogNumber: number, battleSeahorse: number, laserShark: number }>(
    { seaDogNumber: 0, battleSeahorse: 0, laserShark: 0 }
  );

  constructor(private http: HttpClient, toastService: ToastService, helper: Helpers) {
    super(helper, toastService);
  }

  get mercenaries(): Observable<{ seaDogNumber: number, battleSeahorse: number, laserShark: number }> {
    return this._mercenaries.asObservable();
  }

  get round(): Observable<number> {
    this.getCurrentRound();
    return this._round.asObservable();
  }

  get rank(): Observable<number> {
    this.getRank();
    return this._rank.asObservable();
  }

  get country(): Observable<Country> {
    return this._country.asObservable();
  }

  getUserCountry(): Observable<Country> {
    return this.http.get<Country>(environment.apiUrl + this.specAPI, super.header()).pipe(
      map(country => {
        this._country.next(country);
        return country;
      }),
      catchError(error => super.handleError(error))
    );
  }

  getCurrentRound(): Observable<number> {
    return this.http.get<number>(environment.apiUrl + 'countries/round', super.header()).pipe(
      map(round => {
        this._round.next(round);
        return round;
      }),
      catchError(error => super.handleError(error))
    );
  }

  getDevRound(): Observable<number> {
    return this.http.get<number>(environment.apiUrl + this.specAPI + '/devround', super.header()).pipe(
      map(round => {
        return round;
      }),
      catchError(error => super.handleError(error))
    );
  }

  getBuildRound(): Observable<number> {
    return this.http.get<number>(environment.apiUrl + this.specAPI + '/buildround', super.header()).pipe(
      map(round => {
        return round;
      }),
      catchError(error => super.handleError(error))
    );
  }

  getBuildingName(): Observable<number> {
    return this.http.get<{ buildingname: number }>(environment.apiUrl + this.specAPI + '/buildingname', super.header()).pipe(
      map(resp => {
        return resp.buildingname;
      }),
      catchError(error => super.handleError(error))
    );
  }

  getDevelopingName(): Observable<number> {
    return this.http.get<{ developingname: number }>(environment.apiUrl + this.specAPI + '/developingname', super.header()).pipe(
      map(resp => {
        return resp.developingname;
      }),
      catchError(error => super.handleError(error))
    );
  }

  getInhibitant(): Observable<number> {
    return this.http.get<{ inhibitant: number }>(environment.apiUrl + this.specAPI + '/inhabitant', super.header()).pipe(
      map(response => {
        return response.inhibitant;
      }),
      catchError(error => super.handleError(error))
    );
  }

  getPearl(): Observable<number> {
    return this.http.get<{ pearl: number }>(environment.apiUrl + this.specAPI + '/pearl', super.header()).pipe(
      map(response => {
        return response.pearl;
      }),
      catchError(error => super.handleError(error))
    );
  }

  getBuildings(): Observable<{ flowControllNumber: number, reefCastleNumber: number }> {
    return this.http.get<{ flowControllNumber: number, reefCastleNumber: number }>
      (environment.apiUrl + this.specAPI + '/buildings', super.header()).pipe(
        map(response => {
          return response;
        }),
        catchError(error => super.handleError(error))
      );
  }

  getDevelopments(): Observable<{
    mudTractor: boolean, sludgeharvester: boolean, coralWall: boolean,
    sonarGun: boolean, underwaterMaterialArts: boolean, alchemy: boolean
  }> {
    return this.http.get<{
      mudTractor: boolean, sludgeharvester: boolean, coralWall: boolean,
      sonarGun: boolean, underwaterMaterialArts: boolean, alchemy: boolean
    }>
      (environment.apiUrl + this.specAPI + '/developments', super.header()).pipe(
        map(response => {
          return response;
        }),
        catchError(error => super.handleError(error))
      );
  }

  getMercenaries(): Observable<{ seaDogNumber: number, battleSeahorse: number, laserShark: number }> {
    return this.http.get<{ seaDogNumber: number, battleSeahorse: number, laserShark: number }>
      (environment.apiUrl + this.specAPI + '/mercenaries', super.header()).pipe(
        map(response => {
          this._mercenaries.next(response);
          return response;
        }),
        catchError(error => super.handleError(error))
      );
  }

  getPoints(): Observable<number> {
    return this.http.get<{ points: number }>(environment.apiUrl + this.specAPI + '/points', super.header()).pipe(
      map(response => {
        return response.points;
      }),
      catchError(error => super.handleError(error))
    );
  }

  getRank(): Observable<number> {
    return this.http.get<{ rank: number }>(environment.apiUrl + this.specAPI + '/rank', super.header()).pipe(
      map(response => {
        this._rank.next(response.rank);
        return response.rank;
      }),
      catchError(error => super.handleError(error))
    );
  }

  next(): Observable<{ country: Country, round: number }> {
    return this.http.get<{ country: Country, round: number }>(environment.apiUrl + 'countries/next', super.header()).pipe(
      map(response => {
        this._country.next(response.country);
        this._round.next(response.round);
        return response;
      }),
      catchError(error => super.handleError(error))
    );
  }

  putCountry(country: Country): Observable<any> {
    return this.http.put<Country>(environment.apiUrl + 'countries/' + country.id, country, super.header()).pipe(
      catchError(error => super.handleError(error))
    );
  }

  postBuildAction(buildingType: number): Observable<any> {
    return this.http.post<number>(environment.apiUrl + this.specAPI + '/build', buildingType, super.header()).pipe(
      catchError(error => super.handleError(error))
    );
  }

  postDevelopAction(developType: number): Observable<any> {
    return this.http.post<number>(environment.apiUrl + this.specAPI + '/develop', developType, super.header()).pipe(
      catchError(error => super.handleError(error))
    );
  }

  postHireAction(mercanryList: MercenaryRequest): Observable<any> {
    return this.http.post<{ message: string }>(environment.apiUrl + this.specAPI + '/hire', mercanryList,
      { headers: super.header().headers, observe: 'response' }).pipe(
        map(response => {
          console.log(response);
        }),
        catchError(error => super.handleError(error))
      );
  }
}
