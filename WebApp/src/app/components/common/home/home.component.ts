import {Component, OnInit} from '@angular/core';
import {BehaviorSubject, filter, first, merge} from "rxjs";
import {CookBookClient, CuisineCategory} from "../../../../api/CoobBookClient";
import {MatDialog} from "@angular/material/dialog";
import {AddEditDialogComponent} from "../../../dialogs/add-edit-dialog/add-edit-dialog.component";
import {Router} from "@angular/router";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {
  refreshSbj = new BehaviorSubject<boolean>(true);
  cuisineCategories: CuisineCategory[] = []
  displayedColumns: string[] = [];

  constructor(private api: CookBookClient,
              private dialog: MatDialog,
              private router: Router) {
    merge(this.refreshSbj)
      .subscribe(() => {
        api.categoryAll()
          .pipe(first())
          .subscribe(x => this.cuisineCategories = x);
      })
  }

  ngOnInit(): void {
    this.displayedColumns = ['name', 'actions'];
  }

  addCuisineCategory() {
    this.dialog.open(AddEditDialogComponent, {
      width: '400px',
      height: '230px',
      data: {
        fieldsName: ['Наименование'],
        fields: ['name'],
        name: 'Добавить категорию кухни'
      },
    })
      .afterClosed()
      .pipe(first(), filter(x => !!x))
      .subscribe(data => {
        console.log(data);
        this.api.categoryPOST(new CuisineCategory(data))
          .subscribe(() => this.refreshSbj.next(true));
      });
  }

  deleteCuisineCategory(row: CuisineCategory) {
    this.api.categoryDELETE(row.id)
      .subscribe(() => this.refreshSbj.next(true))
  }

  editCuisineCategory(row: CuisineCategory) {
    this.dialog.open(AddEditDialogComponent, {
      width: '400px',
      height: '230px',
      data: {
        fieldsName: ['Наименование'],
        fields: ['name'],
        name: 'Изменить категорию кухни',
        data: row
      },
    })
      .afterClosed()
      .pipe(first(), filter(x => !!x))
      .subscribe(data => {
        data.id = row.id;
        this.api.categoryPUT(data)
          .subscribe(() => this.refreshSbj.next(true));
      });
  }

  async openCuisineCategory(row: CuisineCategory, event: any) {
    if (event.target.tagName.toLowerCase() !== 'td') {
      return;
    }
    await this.router.navigate(['dish-type', row.id]);
  }
}
