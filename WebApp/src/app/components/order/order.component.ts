import {Component, OnInit} from '@angular/core';
import {CookBookClient, OrderDetailData} from "../../../api/CoobBookClient";
import {BehaviorSubject, first, merge} from "rxjs";
import {MatDialog} from "@angular/material/dialog";
import {OrderCompleteComponent} from "../../dialogs/order-complete/order-complete.component";

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.sass']
})
export class OrderComponent implements OnInit {
  refreshSbj = new BehaviorSubject<boolean>(true);
  displayedColumns: string[] = ['name', 'price', 'quantity', 'actions'];
  orderDetails: OrderDetailData[] = [];

  constructor(private api: CookBookClient,
              private dialog: MatDialog) {
    merge(this.refreshSbj)
      .subscribe(() => {
        this.api.recipesForOrder()
          .subscribe(data => {
            // @ts-ignore
            this.orderDetails = data;
          })
      })
  }

  ngOnInit(): void {
  }

  addQuantity(element: OrderDetailData) {
    // @ts-ignore
    this.orderDetails[this.orderDetails.indexOf(element)].quantity++;
  }

  deleteQuantity(element: OrderDetailData) {
    // @ts-ignore
    if (this.orderDetails[this.orderDetails.indexOf(element)].quantity > 0) {
      // @ts-ignore
      this.orderDetails[this.orderDetails.indexOf(element)].quantity--;
    }
  }

  createOrder() {
    this.api.addOrder(this.orderDetails)
      .subscribe(() => {
        this.dialog.open(OrderCompleteComponent, {
          width: '400px',
          height: '150px'
        })
          .afterClosed()
          .pipe(first())
          .subscribe(() => this.refreshSbj.next(true))
      })
  }
}
