import {IBaseEntity} from "./IBaseEntity";

export interface IProductSold extends IBaseEntity {
  quantity: number,
  productId: number,
  productName: string,
  saleId: number,
  saleDescription: string,
  productSoldTime: Date,
  dateString: string
}
