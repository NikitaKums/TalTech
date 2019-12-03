import {IBaseEntity} from "./IBaseEntity";

export interface IProductSoldProductSaleIdName extends IBaseEntity{
  productId: number,
  productName: string,
  saleId: number,
  saleDescription: string
}
