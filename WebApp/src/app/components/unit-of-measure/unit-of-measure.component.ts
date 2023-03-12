import { Component, OnInit } from '@angular/core';
import {CookBookClient, DishType, UnitOfMeasure} from "../../../api/CoobBookClient";
import {AddEditDialogComponent} from "../../dialogs/add-edit-dialog/add-edit-dialog.component";
import {BehaviorSubject, filter, first, merge} from "rxjs";
import {MatDialog} from "@angular/material/dialog";
import {Router} from "@angular/router";

@Component({
  selector: 'app-unit-of-measure',
  templateUrl: './unit-of-measure.component.html',
  styleUrls: ['./unit-of-measure.component.sass']
})
export class UnitOfMeasureComponent implements OnInit {
  refreshSbj = new BehaviorSubject<boolean>(true);
  displayedColumns: string[] = [];
  unitOfMeasures: UnitOfMeasure[] = [];

  constructor(private api: CookBookClient,
              private dialog: MatDialog) {
    merge(this.refreshSbj)
      .subscribe(() => {
        api.unitOfMeasureAll()
          .pipe(first())
          .subscribe(x => this.unitOfMeasures = x);
      })
  }

  ngOnInit(): void {
    this.displayedColumns = ['name', 'actions'];
  }

  addUnitOfMeasure() {
    this.dialog.open(AddEditDialogComponent, {
      width: '400px',
      height: '230px',
      data: {
        fieldsName: ['Наименование'],
        fields: ['name'],
        name: 'Добавить единицу измерения'
      },
    })
      .afterClosed()
      .pipe(first(), filter(x => !!x))
      .subscribe(data => {
        this.api.unitOfMeasurePOST(data)
          .subscribe(() => this.refreshSbj.next(true));
      });
  }

  editUnitOfMeasure(element: UnitOfMeasure) {
    this.dialog.open(AddEditDialogComponent, {
      width: '400px',
      height: '230px',
      data: {
        fieldsName: ['Наименование'],
        fields: ['name'],
        name: 'Изменить единицу измерения',
        data: element
      },
    })
      .afterClosed()
      .pipe(first(), filter(x => !!x))
      .subscribe(data => {
        data.id = element.id;
        this.api.unitOfMeasurePUT(data)
          .subscribe(() => this.refreshSbj.next(true));
      });
  }

  deleteUnitOfMeasure(element: UnitOfMeasure) {
    this.api.unitOfMeasureDELETE(element.id)
      .subscribe(() => this.refreshSbj.next(true))
  }
}
