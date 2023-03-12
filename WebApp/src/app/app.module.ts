import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {RouterModule, Routes} from '@angular/router'
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {HomeComponent} from './components/common/home/home.component';
import {HeaderComponent} from './components/common/header/header.component';
import {FooterComponent} from './components/common/footer/footer.component';
import {GroceryItemComponent} from './components/grocery-item/grocery-item.component';
import {DishRecipeComponent} from './components/dish-recipe/dish-recipe.component';
import {DishTypeComponent} from './components/dish-type/dish-type.component';
import {RecipeIngredientComponent} from './components/recipe-ingredient/recipe-ingredient.component';
import {UnitOfMeasureComponent} from './components/unit-of-measure/unit-of-measure.component';
import {NotFoundComponent} from './components/common/not-found/not-found.component';
import {API_BASE_URL, CookBookClient} from "../api/CoobBookClient";
import {environment} from "../environments/environment";
import {MatSelectModule} from "@angular/material/select";
import {HttpClientModule} from "@angular/common/http";
import {MatGridListModule} from "@angular/material/grid-list";
import {MatTableModule} from "@angular/material/table";
import {AddEditDialogComponent} from './dialogs/add-edit-dialog/add-edit-dialog.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatInputModule} from "@angular/material/input";
import {MatDialogModule} from "@angular/material/dialog";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {AddEditGroceryItemComponent} from './dialogs/add-edit-grocery-item/add-edit-grocery-item.component';
import { AddEditIngredientComponent } from './dialogs/add-edit-ingredient/add-edit-ingredient.component';
import { OrderComponent } from './components/order/order.component';
import {MatCheckboxModule} from "@angular/material/checkbox";
import { OrderCompleteComponent } from './dialogs/order-complete/order-complete.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  }, {
    path: 'dish-recipe/:cuisineCategoryId/:dishTypeId',
    component: DishRecipeComponent
  }, {
    path: 'grocery-item',
    component: GroceryItemComponent
  }, {
    path: 'dish-type/:cuisineCategoryId',
    component: DishTypeComponent
  }, {
    path: 'recipe-ingredient/:cuisineCategoryId/:dishTypeId/:recipeId',
    component: RecipeIngredientComponent
  }, {
    path: 'recipe-ingredient/:cuisineCategoryId/:dishTypeId',
    component: RecipeIngredientComponent
  },{
    path: 'unit-of-measure',
    component: UnitOfMeasureComponent
  }, {
    path: 'order',
    component: OrderComponent
  }, {
    path: '**',
    component: NotFoundComponent
  }
]

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    FooterComponent,
    GroceryItemComponent,
    DishRecipeComponent,
    DishTypeComponent,
    RecipeIngredientComponent,
    UnitOfMeasureComponent,
    NotFoundComponent,
    AddEditDialogComponent,
    AddEditGroceryItemComponent,
    AddEditIngredientComponent,
    OrderComponent,
    OrderCompleteComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(routes),
    MatSelectModule,
    MatGridListModule,
    MatTableModule,
    FormsModule,
    MatInputModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatIconModule,
    MatCheckboxModule
  ],
  providers: [
    {provide: API_BASE_URL, useValue: environment.apiUrl},
    CookBookClient
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
