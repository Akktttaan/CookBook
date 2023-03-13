import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {OrderData, OrderDetailData, RecipeIngredientData} from "../../../api/CoobBookClient";

@Component({
  selector: 'app-order-complete',
  templateUrl: './order-complete.component.html',
  styleUrls: ['./order-complete.component.sass']
})
export class OrderCompleteComponent implements OnInit {

  orderReport: OrderData;
  constructor(
    public dialogRef: MatDialogRef<OrderCompleteComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    this.orderReport = data.data;
  }

  ngOnInit(): void {
  }

  onSubmit() {
    this.dialogRef.close();
  }

  getGroceryItemPrice(order: OrderDetailData, item: RecipeIngredientData): number{
    // @ts-ignore
    return item.groceryItem?.price * item.quantity * order.quantity;
  }

  getRecipePrice(item: OrderDetailData){
    // @ts-ignore
    return item.dishRecipe?.recipeIngredients?.reduce((acc, value) => acc + (value.groceryItem.price * value.quantity), 0) * item.quantity * item.dishRecipe?.margin
  }

  getGroceryItemQuantity(order: OrderDetailData, item: RecipeIngredientData){
    // @ts-ignore
    return item.quantity * order.quantity;
  }
}
