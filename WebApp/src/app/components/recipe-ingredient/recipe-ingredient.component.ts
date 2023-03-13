import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder} from "@angular/forms";
import {filter, first} from "rxjs";
import {CookBookClient, DishRecipeData, GroceryItem, RecipeIngredientData} from "../../../api/CoobBookClient";
import {MatDialog} from "@angular/material/dialog";
import {ActivatedRoute, Router} from "@angular/router";
import {AddEditIngredientComponent} from "../../dialogs/add-edit-ingredient/add-edit-ingredient.component";
import {MatTable} from "@angular/material/table";

@Component({
  selector: 'app-recipe-ingredient',
  templateUrl: './recipe-ingredient.component.html',
  styleUrls: ['./recipe-ingredient.component.sass']
})
export class RecipeIngredientComponent implements OnInit {
  recipeIngredients: RecipeIngredientData[] = [];
  dishTypeId: number;
  cuisineCategoryId: number;
  recipeId: number;
  groceryItems: GroceryItem[] = []
  dataForm = this.formBuilder.group({
    id: [null],
    name: [null],
    margin: [null],
    cuisineCategoryId: [null],
    dishTypeId: [null]
  })
  displayedColumns: string[] = [];
  // @ts-ignore
  @ViewChild(MatTable) table: MatTable<RecipeIngredientData>;

  constructor(
    private formBuilder: FormBuilder,
    private api: CookBookClient,
    private dialog: MatDialog,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.dishTypeId = this.route.snapshot.params['dishTypeId'];
    this.cuisineCategoryId = this.route.snapshot.params['cuisineCategoryId']
    this.recipeId = this.route.snapshot.params['recipeId'];
    if (!!this.recipeId) {
      this.api.getRecipe(this.recipeId)
        .subscribe(data => {
          // @ts-ignore
          this.dataForm.patchValue(data);
        });
      this.api.ingredientAll(this.recipeId)
        .pipe(first())
        .subscribe(data => this.recipeIngredients = data)
    }

    api.itemAll()
      .subscribe(data => this.groceryItems = data)
  }

  ngOnInit(): void {
    this.displayedColumns = ['groceryItem', 'quantity', 'actions'];
  }


  onSubmit() {
    // @ts-ignore
    const model = new DishRecipeData(this.dataForm.getRawValue());
    model.recipeIngredients = []
    this.recipeIngredients.forEach(x => {
      model.recipeIngredients?.push(new RecipeIngredientData(x))
    })
    if(!!this.recipeId){
      this.api.recipePUT(model)
        .subscribe(() => {
          this.router.navigate(['dish-recipe', this.cuisineCategoryId, this.dishTypeId])
        });
    }
    else{
      model.cuisineCategoryId = this.cuisineCategoryId;
      model.dishTypeId = this.dishTypeId;
      this.api.recipePOST(model)
        .subscribe((x) => {
          this.router.navigate(['dish-recipe', this.cuisineCategoryId, this.dishTypeId])
        })
    }
  }

  addRecipeIngredient() {
    this.dialog.open(AddEditIngredientComponent, {
      width: '400px',
      data: {
        title: 'Добавить ингридиент',
        groceryItems: this.groceryItems
      }
    })
      .afterClosed()
      .pipe(first(), filter(x => !!x))
      .subscribe((data: RecipeIngredientData) => {
        if(!!this.recipeId){
          data.dishRecipeId = this.recipeId;
        }
        this.recipeIngredients.push(data)
        this.table.renderRows()
      });
  }

  editRecipeIngredient(element: RecipeIngredientData) {
    this.dialog.open(AddEditIngredientComponent, {
      width: '400px',
      data: {
        title: 'Изменить ингридиент',
        groceryItems: this.groceryItems,
        data: element
      }
    })
      .afterClosed()
      .pipe(first(), filter(x => !!x))
      .subscribe(data => {
        this.recipeIngredients[this.recipeIngredients.indexOf(element)] = data;
        this.table.renderRows()
      });
  }

  deleteRecipeIngredient(element: RecipeIngredientData) {
    this.recipeIngredients.splice(this.recipeIngredients.indexOf(element), 1)
    this.table.renderRows()
  }
}
