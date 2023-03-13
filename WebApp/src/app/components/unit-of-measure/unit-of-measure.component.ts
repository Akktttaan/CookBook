import {Component, OnInit} from '@angular/core';
import {CookBookClient, UnitOfMeasure} from "../../../api/CoobBookClient";
import {AddEditDialogComponent} from "../../dialogs/add-edit-dialog/add-edit-dialog.component";
import {BehaviorSubject, filter, first, merge, Subject, tap} from "rxjs";
import {MatDialog} from "@angular/material/dialog";
import {ErrorHandlerService} from "../../dialogs/error/error-handler.service";

@Component({
  selector: 'app-unit-of-measure',
  templateUrl: './unit-of-measure.component.html',
  styleUrls: ['./unit-of-measure.component.sass']
})
export class UnitOfMeasureComponent implements OnInit {
  errorSbj = new Subject<string>()
  refreshSbj = new BehaviorSubject<boolean>(true);
  displayedColumns: string[] = [];
  unitOfMeasures: UnitOfMeasure[] = [];

  constructor(private api: CookBookClient,
              private dialog: MatDialog,
              private error: ErrorHandlerService) {
    merge(this.refreshSbj)
      .subscribe(() => {
        api.unitOfMeasureAll()
          .pipe(first())
          .subscribe(x => this.unitOfMeasures = x);
      })

    this.error.showError(this.errorSbj);
  }

  ngOnInit(): void {
    this.displayedColumns = ['name', 'actions'];
  }

  addUnitOfMeasure() {
    this.dialog.open(AddEditDialogComponent, {
      width: '400px',
      data: {
        fieldsName: ['Наименование'],
        fields: ['name'],
        name: 'Добавить единицу измерения'
      },
    })
      .afterClosed()
      .pipe(
        first(),
        tap((x: UnitOfMeasure) => {
          if(this.unitOfMeasures.map(x => x.name).includes(x.name)){
            this.errorSbj.next("Такая единица измерения уже существует!")
          }
        }),
        filter(x => !!x && this.unitOfMeasures.map(x => x.name).includes(x.name))
      )
      .subscribe(data => {
        this.api.unitOfMeasurePOST(data)
          .subscribe(() => this.refreshSbj.next(true));
      });
  }

  editUnitOfMeasure(element: UnitOfMeasure) {
    this.dialog.open(AddEditDialogComponent, {
      width: '400px',
      data: {
        fieldsName: ['Наименование'],
        fields: ['name'],
        name: 'Изменить единицу измерения',
        data: element
      },
    })
      .afterClosed()
      .pipe(
        first(),
        tap((x: UnitOfMeasure) => {
          if(this.unitOfMeasures.map(x => x.name).includes(x.name)){
            this.errorSbj.next("Такая единица измерения уже существует!")
          }
        }),
        filter(x => !!x && this.unitOfMeasures.map(x => x.name).includes(x.name))
      )
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
