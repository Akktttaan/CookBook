import {Component, OnInit} from '@angular/core';
import {CookBookClient, DishType} from "../../../api/CoobBookClient";
import {AddEditDialogComponent} from "../../dialogs/add-edit-dialog/add-edit-dialog.component";
import {BehaviorSubject, filter, first, merge} from "rxjs";
import {MatDialog} from "@angular/material/dialog";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-dish-type',
  templateUrl: './dish-type.component.html',
  styleUrls: ['./dish-type.component.sass']
})
export class DishTypeComponent implements OnInit {
  refreshSbj = new BehaviorSubject<boolean>(true);
  dishTypes: DishType[] = [];
  displayedColumns: string[] = [];

  constructor(private api: CookBookClient,
              private dialog: MatDialog,
              private router: Router,
              private route: ActivatedRoute) {
    merge(this.refreshSbj)
      .subscribe(() => {
        api.dishTypeAll()
          .pipe(first())
          .subscribe(x => this.dishTypes = x);
      })
  }

  ngOnInit(): void {
    this.displayedColumns = ['name', 'actions'];
  }

  addDishType() {
    this.dialog.open(AddEditDialogComponent, {
      width: '400px',
      height: '230px',
      data: {
        fieldsName: ['Наименование'],
        fields: ['name'],
        name: 'Добавить тип блюда'
      },
    })
      .afterClosed()
      .pipe(first(), filter(x => !!x))
      .subscribe(data => {
        this.api.dishTypePOST(data)
          .subscribe(() => this.refreshSbj.next(true));
      });
  }

  editDishType(element: DishType) {
    this.dialog.open(AddEditDialogComponent, {
      width: '400px',
      height: '230px',
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
