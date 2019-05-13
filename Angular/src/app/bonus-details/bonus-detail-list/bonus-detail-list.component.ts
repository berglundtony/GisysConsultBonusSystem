import { Component, OnInit, ViewChild, TemplateRef, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { BonusDetailService } from 'src/app/shared/bonus-detail.service';
import { BonusDetail } from 'src/app/shared/bonus-detail.model';
import { DatePipe } from '@angular/common';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable, Subject } from 'rxjs';
import { DataTableDirective } from 'angular-datatables'
import { Router } from '@angular/router';

@Component({
  selector: 'app-bonus-detail-list',
  templateUrl: './bonus-detail-list.component.html',
  styleUrls: ['./bonus-detail-list.component.css']
})
export class BonusDetailListComponent implements OnInit, OnDestroy {

  // For the FormControl -Adding BonusDetails
  insertForm: FormGroup;
  ConsultID: FormControl;
  ChargedHours: FormControl;
  NetResult: FormControl;

  //Add Modal
  @ViewChild('template') addmodal: TemplateRef<any>;

  //Modal properties
  modalMessage: string;
  modalRef: BsModalRef;
  selectedBonusDetail: BonusDetail;
  bonusdetails$ : Observable<BonusDetail[]>;
  bonusdetails : BonusDetail[] = [];

  //Datatables properties
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject();

  @ViewChild(DataTableDirective) dtElement: DataTableDirective;

  constructor(private service: BonusDetailService, private modalService : BsModalService, private fb: FormBuilder,
     private chRef: ChangeDetectorRef, private router: Router, private datePipe: DatePipe) { }

  onAddBonusDetail(){
    this.modalRef = this.modalService.show(this.addmodal);
  }

  onSubmit(){
    let newBonus = this.insertForm.value;
    this.service.insertBonusDetails(newBonus).subscribe(
      result => {
        this.service.clearCache();
        this.bonusdetails$ = this.service.getBonuses();
        this.bonusdetails$.subscribe(newlist =>{
          this.bonusdetails = newlist;
          this.modalRef.hide();
          this.insertForm.reset();
          this.rerender();
        });
        console.log("New BonusDetail Added");
      },
      error => {
      this.service.clearCache();
        this.bonusdetails$ = this.service.getBonuses();
        this.bonusdetails$.subscribe(newlist =>{
          this.bonusdetails = newlist;
          this.insertForm.reset();
         this.rerender(); 
        });
       // console.log("Could not add a BonusDetail");
       this.modalMessage = "Denna konsult är redan bonus registrerad";
      }
    )
  }

  rerender(){
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) =>
    {
      dtInstance.destroy();
      this.dtTrigger.next();
    })
  }
  onDelete(bonusDetail: BonusDetail):void
  {
    if (confirm('Are you sure to delete this record ?')) {
      this.service.deleteBonusDetail(bonusDetail.BonusID).subscribe(result => {
        this.service.clearCache();
        this.bonusdetails$ = this.service.getBonuses();
        this.bonusdetails$.subscribe(newlist =>
        {
            this.bonusdetails = newlist;
            this.rerender();
        })
      })
    }
  }

  onSelect(bonusDetail: BonusDetail): void
  {
      this.selectedBonusDetail = bonusDetail;
      console.log( this.selectedBonusDetail )
      this.router.navigateByUrl("/bonus/" + bonusDetail.BonusID)
  }

  ngOnInit() {
    //this.service.refreshList();
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 8,
      autoWidth: true,
      order:[[0,'desc']]
    };
    this.bonusdetails$ = this.service.getBonuses();
    this.bonusdetails$.subscribe(result => 
      { this.bonusdetails = result;
        this.chRef.detectChanges();
        this.dtTrigger.next();
    });
      //Modal message
      this.modalMessage = "Alla fälten skall fyllas i";

      //Initialize add bonus-detail
      this.ConsultID = new FormControl('', [Validators.required, Validators.min(0), Validators.max(9000)]),
      this.NetResult = new FormControl('', [Validators.required, Validators.min(0), Validators.max(10000000)]),
      this.ChargedHours = new FormControl('', [Validators.required, Validators.min(0), Validators.max(600)])
      this.insertForm = this.fb.group({
          ConsultID: this.ConsultID,
          NetResult: this.NetResult,
          ChargedHours: this.ChargedHours,
    })
  }

  ngOnDestroy(){
    this.dtTrigger.unsubscribe();
  }
}
