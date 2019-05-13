import { Component, OnInit } from '@angular/core';
import { ConsultDetailService } from 'src/app/shared/consult-detail.service';
import { NgForm} from '@angular/forms';
import { DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-consult-detail',
  templateUrl: './consult-detail.component.html',
  styleUrls: ['./consult-detail.component.css']
})
export class ConsultDetailComponent implements OnInit {
  constructor(private service: ConsultDetailService, private toastr: ToastrService, private datePipe: DatePipe) { }

  ngOnInit() {
    this.resetForm();  
  }

  onChange(event){
    this.service.formData.EmploymentDate = event;
  }

  resetForm(form?:NgForm){
    if(form!=null)
    form.resetForm();
    this.service.formData = {
      ConsultID :0,
      FirstName :'',
      LastName :'',
      EmploymentDate: new Date()
  }
}

onSubmit(form:NgForm){
  if(this.service.formData.ConsultID==0){
    this.insertRecord(form);
  }
  else{
    this.updateRecord(form);
  }
}

 insertRecord(form:NgForm){   
  this.service.postConsultDetail().subscribe(
    res => {
      this.resetForm(form);
      this.toastr.success('Submitted successfully', 'Consult Register');
      this.service.refreshList();
    },
    err => {
      console.log(err);
    }
  )
 }
 
  updateRecord(form:NgForm){
    this.service.putConsultDetail().subscribe(
      res => {
        this.resetForm(form);
        this.toastr.info('Submitted successfully', 'Consult Register')
        this.service.refreshList();
      },
      err => {
        console.log(err);
      }
    )
  }  
 }

 
