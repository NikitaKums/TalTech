import {IBaseEntity} from "./IBaseEntity";

export interface IProductReturned extends IBaseEntity {
  quantity: number,
  productReturnedTime: Date,
  productId: number,
  productName: string,
  returnId: number,
  returnDescription: string,
  dateString: string,
}
