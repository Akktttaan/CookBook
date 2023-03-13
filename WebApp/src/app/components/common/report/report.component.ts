import {Component, OnInit} from '@angular/core';
import {CookBookClient, ReportData} from "../../../../api/CoobBookClient";
import {FormBuilder} from "@angular/forms";
import {first} from "rxjs";

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.sass']
})
export class ReportComponent implements OnInit {
  // @ts-ignore
  allOrder = new ReportData();
  // @ts-ignore

  dataForm = this.builder.group({
    dateFrom: [null],
    dateTo: [null]
  })

  constructor(private api: CookBookClient,
              private builder: FormBuilder) {
  }

  ngOnInit(): void {
  }

  onSubmit() {
    // @ts-ignore
    this.api.allOrder(this.dataForm.controls.dateFrom.value, this.dataForm.controls.dateTo.value)
      .pipe(first())
      .subscribe(data => {
        console.log(data);
        this.allOrder = data;
      })
  }
}
