import { Component, OnInit, Input } from '@angular/core';
import { BonusDetailService } from 'src/app/shared/bonus-detail.service';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { HttpClient } from "@angular/common/http";
import { Router, ActivatedRoute } from '@angular/router';
import { BonusDetail } from 'src/app/shared/bonus-detail.model';

@Component({
  selector: 'app-bonus-detail',
  templateUrl: './bonus-detail.component.html',
  styleUrls: ['./bonus-detail.component.css']
})
export class BonusDetailComponent implements OnInit {

  @Input() bonusDetail: BonusDetail;

  constructor(private service: BonusDetailService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
      let id =+ this.route.snapshot.params['id'];
      this.service.getBonusById(id).subscribe(result =>{
        this.bonusDetail = result
      });
  }

  onBackToList(): void
  {
      this.router.navigateByUrl("/bonus");
  }

}
