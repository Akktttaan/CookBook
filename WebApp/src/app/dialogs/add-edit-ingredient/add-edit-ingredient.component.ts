import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {FormBuilder} from "@angular/forms";
import {CookBookClient} from "../../../api/CoobBookClient";

@Component({
  selector: 'app-add-edit-ingredient',
  templateUrl: './add-edit-ingredient.component.html',
  styleUrls: ['./add-edit-ingredient.component.sass']
})
export class AddEditIngredientComponent implements OnInit {
  dataForm = this.formBuilder.group({
    id: [null],
    dishTypeId: [null],
    cuisineCategoryId: [null],
    groceryItemDescription: [''],
    groceryItemId: [null],
    quantity: [null],
    groceryItemUnitOfMeasure: ['']
  })
  constructor(
    public dialogRef: MatDialogRef<AddEditIngredientComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private formBuilder: FormBuilder,
    private api: CookBookClient
  ) {
    if(!!data.data){
      this.dataForm.patchValue(data.data);
    }
  }

  ngOnInit(): void {
  }

  onSubmit() {
    // @ts-ignore
    this.api.itemDesc(this.dataForm.controls.groceryItemId.value)
      .subscribe(data => {
        // @ts-ignore
        this.dataForm.controls.groceryItemDescription.setValue(data.name);
        // @ts-ignore
        this.dataForm.controls.groceryItemUnitOfMeasure.setValue(data.unitOfMeasureDescription)
        this.dialogRef.close(this.dataForm.getRawValue());
      })
  }

  onNoClick() {
    this.dialogRef.close();
  }
}
