import {Component, OnInit, ViewChild} from '@angular/core';
import {CookBookClient, DishRecipe, GroceryItem} from "../../../api/CoobBookClient";
import {BehaviorSubject, first, merge} from "rxjs";
import {MatDialog} from "@angular/material/dialog";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-dish-recipe',
  templateUrl: './dish-recipe.component.html',
  styleUrls: ['./dish-recipe.component.sass']
})
export class DishRecipeComponent implements OnInit {
  refreshSbj = new BehaviorSubject<boolean>(true);
  dishRecipes: DishRecipe[] = [];
  displayedColumns: string[] = [];
  groceryItems: GroceryItem[] = [];
  dishTypeId: number;
  cuisineCategoryId: number;

  constructor(private api: CookBookClient,
              private dialog: MatDialog,
              private route: ActivatedRoute,
              private router: Router) {
    this.dishTypeId = this.route.snapshot.params['dishTypeId'];
    this.cuisineCategoryId = this.route.snapshot.params['cuisineCategoryId']
    merge(this.refreshSbj)
      .subscribe(() => {
        api.recipeAll(this.dishTypeId,this.cuisineCategoryId)
          .pipe(first())
          .subscribe(x => this.dishRecipes = x);
      })
    api.itemAll()
      .subscribe(data => this.groceryItems = data)
  }

  ngOnInit(): void {
    this.displayedColumns = ['name', 'price', 'actions'];
  }

  async addDishRecipe() {
    await this.router.navigate(['recipe-ingredient', this.cuisineCategoryId, this.dishTypeId])
  }

  async editDishRecipe(element: DishRecipe) {
    await this.router.navigate(['recipe-ingredient', this.cuisineCategoryId, this.dishTypeId, element.id])
  }

  deleteDishRecipe(element: DishRecipe) {
    this.api.recipeDELETE(element.id)
      .subscribe(() => this.refreshSbj.next(true))
  }
}
