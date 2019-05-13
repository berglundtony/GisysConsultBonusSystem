import { ConsultDetailService } from 'src/app/shared/consult-detail.service';
import { ConsultDetail} from 'src/app/shared/consult-detail.model'
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { DatePipe } from '@angular/common';


@Component({
  selector: 'app-consult-detail-list',
  templateUrl: './consult-detail-list.component.html',
  styleUrls: ['./consult-detail-list.component.css']
})
export class ConsultDetailListComponent implements OnInit {
  constructor(private service: ConsultDetailService, private toastr: ToastrService, private datePipe: DatePipe) { }

  ngOnInit() {
    this.service.refreshList();
  }

  PopulateForm(pd: ConsultDetail) {
    this.service.formData = Object.assign({}, pd);
  }

  onDelete(ConsultID) {
    if (confirm('Are you sure to delete this record ?')) {
      this.service.deleteConsultDetail(ConsultID)
        .subscribe(res => {
          this.service.refreshList();
          this.toastr.warning('Deleted successfully', 'Payment Detail Register');
        },
          err => {
            debugger;
            console.log(err);
          })
    }
  } 

}
