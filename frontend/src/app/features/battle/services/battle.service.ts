import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/shared/services/base.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { Battle } from '../models/battle';
import { ToastService } from 'src/app/shared/services/toast.service';
import { Helpers } from 'src/app/utils/helper';
import { MercenaryRequest } from '../../country/models/mercenary-request';
import { User } from 'src/app/core/models/user';
import { environment } from 'src/environments/environment';
import { map, catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BattleService extends BaseService {

  private _battles = new BehaviorSubject<Battle[]>([]);

  constructor(private http: HttpClient, toastService: ToastService, helper: Helpers) {
    super(helper, toastService);
  }

  get battles(): Observable<Battle[]> {
    return this._battles.asObservable();
  }

  postBattleAction(army: MercenaryRequest, enemy: User) {
    const battleForPost: Battle = {
      AssaultSeaDog: army.AssaultSeaDog as number,
      LaserShark: army.LaserShark as number,
      BattleSeahorse: army.BattleSeahorse as number,
      EnemyName: enemy.username
    };
    console.log(battleForPost);
    return this.http.post<Battle>(environment.apiUrl + 'battle', battleForPost, super.header()).pipe(
        catchError(error => super.handleError(error))
      );
  }

  getBattles(): Observable<Battle[]> {
    return this.http.get<Battle[]>(environment.apiUrl + '/battle', super.header()).pipe(
      map(response => {
        this._battles.next(response);
        return response;
      }),
      catchError(error => super.handleError(error))
    );
  }
}
