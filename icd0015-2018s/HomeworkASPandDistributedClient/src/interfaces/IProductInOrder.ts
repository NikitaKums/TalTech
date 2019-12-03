import {IBaseEntity} from "./IBaseEntity";

export interface IProductInOrder extends IBaseEntity{
  quantity: number,
  productInOrderPlacingTime: Date,
  productId: number,
  productName: string,
  orderId: number,
  orderDescription: string,
  dateString: string
}
