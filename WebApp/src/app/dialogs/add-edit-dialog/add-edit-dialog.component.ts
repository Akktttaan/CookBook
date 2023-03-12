import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {FormControl, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-add-edit-dialog',
  templateUrl: './add-edit-dialog.component.html',
  styleUrls: ['./add-edit-dialog.component.sass']
})
export class AddEditDialogComponent implements OnInit {
  fieldsName: string[]
  dataForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<AddEditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.fieldsName = data.fieldsName;
    this.dataForm = new FormGroup(data.fields.reduce((group: any, field: string) => {
      group[field] = new FormControl();
      return group;
    }, {}));
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
