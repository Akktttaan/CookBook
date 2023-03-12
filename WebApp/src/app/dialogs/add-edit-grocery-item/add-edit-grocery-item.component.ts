import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {FormBuilder} from "@angular/forms";

@Component({
  selector: 'app-add-edit-grocery-item',
  templateUrl: './add-edit-grocery-item.component.html',
  styleUrls: ['./add-edit-grocery-item.component.sass']
})
export class AddEditGroceryItemComponent implements OnInit {

  dataForm = this.formBuilder.group({
    id: [null],
    name: [null],
    unitOfMeasureId: [null],
    price: [null]
  })
  constructor(
    public dialogRef: MatDialogRef<AddEditGroceryItemComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private formBuilder: FormBuilder
  ) {
      if(!!data.data){
        this.dataForm.patchValue(data.data);
      }
  }

  ngOnInit(): void {
  }

  onSubmit() {
    this.dialogRef.close(this.dataForm.getRawValue());
  }

  onNoClick() {
    this.dialogRef.close();
  }
}
