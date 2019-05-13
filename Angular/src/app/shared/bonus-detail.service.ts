import { Injectable } from '@angular/core';
import { BonusDetail } from './bonus-detail.model';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from 'rxjs';
import { flatMap, first, shareReplay } from 'rxjs/operators';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';


@Injectable({
  providedIn: 'root'
})
export class BonusDetailService {
  constructor(private http:HttpClient) { }
  formData: BonusDetail
  readonly rootURL = 'http://localhost:51302/api';
  list: BonusDetail[];
  private bonusdetails: Observable<BonusDetail[]>;

    // Http Options
    httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    } 

    getBonuses(): Observable<BonusDetail[]>{
      if(!this.bonusdetails){
        this.bonusdetails = this.http.get<BonusDetail[]>(this.rootURL + '/Bonus/' ).pipe(shareReplay());
      }
      return this.bonusdetails;
       
    }

    getBonusById(id: number): Observable<BonusDetail>
    {
        return this.getBonuses().pipe(flatMap(result => result), first(bonus => bonus.BonusID == id));
  
    }

    insertBonusDetails(newBonus: BonusDetail): Observable<BonusDetail> {
        return this.http.post<BonusDetail>(this.rootURL + '/Bonus/', newBonus);
    }

    deleteBonusDetail(id: number): Observable<any>{
      return this.http.delete(this.rootURL + '/Bonus/' + id)
    }
    clearCache()
    {
      this.bonusdetails = null;
    }
}
