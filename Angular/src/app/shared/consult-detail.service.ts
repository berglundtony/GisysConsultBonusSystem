import { Injectable } from '@angular/core';
import { ConsultDetail } from './consult-detail.model';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { forEach } from '@angular/router/src/utils/collection';

@Injectable({
  providedIn: 'root'
})
export class ConsultDetailService {
  formData: ConsultDetail;
  readonly rootURL = 'http://localhost:51302/api';

  list: ConsultDetail[];
  constructor(private http:HttpClient, private datePipe: DatePipe) { }
    postConsultDetail(){  
      var date = new Date(this.formData.EmploymentDate).toISOString();
      this.formData.EmploymentDate = new Date(date); 
      var time = new Date(this.formData.EmploymentDate).setHours(0,0,0,0);
      this.formData.EmploymentDate = new Date(time);
    return this.http.post(this.rootURL + '/Consults', this.formData);
    }
    putConsultDetail() {
    return this.http.put(this.rootURL + '/Consults/' + this.formData.ConsultID, this.formData);
    }
    deleteConsultDetail(id) {
      return this.http.delete(this.rootURL + '/Consults/'+ id);
    }
    refreshList(){
      this.http.get(this.rootURL + '/Consults')
      .toPromise()
      .then(res => this.list = res as ConsultDetail[])
      }
  
    //function for fetching headers, you can define according to your need over here..
    getCommonHeaders(){
      let headers = new Headers();
      headers.append('Content-Type','application/json');
      return headers;
    }

   
}
