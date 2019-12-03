import {IBaseEntity} from "./IBaseEntity";

export interface IOrder extends IBaseEntity{
  description: string,
  orderCreationTime: Date,
  shipperId: number,
  shipperName: string,
  shopId: number,
  shopName: string,
  productsInOrderCount: number,
  dateString: string
}
