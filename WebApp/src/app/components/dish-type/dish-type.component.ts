import {Component, OnInit} from '@angular/core';
import {CookBookClient, DishType} from "../../../api/CoobBookClient";
import {AddEditDialogComponent} from "../../dialogs/add-edit-dialog/add-edit-dialog.component";
import {BehaviorSubject, filter, first, merge, Subject, tap} from "rxjs";
import {MatDialog} from "@angular/material/dialog";
import {ActivatedRoute, Router} from "@angular/router";
import {ErrorHandlerService} from "../../dialogs/error/error-handler.service";

@Component({
  selector: 'app-dish-type',
  templateUrl: './dish-type.component.html',
  styleUrls: ['./dish-type.component.sass']
})
export class DishTypeComponent implements OnInit {
  errorSbj = new Subject<string>();
  refreshSbj = new BehaviorSubject<boolean>(true);
  dishTypes: DishType[] = [];
  displayedColumns: string[] = [];

  constructor(private api: CookBookClient,
              private dialog: MatDialog,
              private router: Router,
              private route: ActivatedRoute,
              private error: ErrorHandlerService) {
    merge(this.refreshSbj)
      .subscribe(() => {
        api.dishTypeAll()
          .pipe(first())
          .subscribe(x => this.dishTypes = x);
      })
    this.error.showError(this.errorSbj);
  }

  ngOnInit(): void {
    this.displayedColumns = ['name', 'actions'];
  }

  addDishType() {
    this.dialog.open(AddEditDialogComponent, {
      width: '400px',
      data: {
        fieldsName: ['Наименование'],
        fields: ['name'],
        name: 'Добавить тип блюда'
      },
    })
      .afterClosed()
      .pipe(
        first(),
        tap((data: DishType) => {
          if(this.dishTypes.map(x => x.name).includes(data.name)){
            this.errorSbj.next("Такой тип блюда уже существует!")
          }
        }),
        filter(x => !!x && !this.dishTypes.map(x => x.name).includes(x.name))
      )
      .subscribe(data => {
        this.api.dishTypePOST(data)
          .subscribe(() => this.refreshSbj.next(true));
      });
  }

  editDishType(element: DishType) {
    this.dialog.open(AddEditDialogComponent, {
      width: '400px',
      data: {
        fieldsName: ['Наименование'],
        fields: ['name'],
        name: 'Изменить тип блюда',
        data: element
      },
    })
      .afterClosed()
      .pipe(first(), filter(x => !!x))
      .subscribe(data => {
        data.id = element.id;
        this.api.dishTypePUT(data)
          .subscribe(() => this.refreshSbj.next(true));
      });
  }

  deleteDishType(element: DishType) {
    this.api.dishTypeDELETE(element.id)
      .subscribe(() => this.refreshSbj.next(true))
  }

  async openDishRecipes(element: DishType, event: any) {
    if (event.target.tagName.toLowerCase() !== 'td') {
      return;
    }
    const {cuisineCategoryId} = this.route.snapshot.params;
    await this.router.navigate(['dish-recipe', cuisineCategoryId, element.id]);
  }
}
