import {IBaseEntity} from "./IBaseEntity";

export interface IIdAndNameProduct extends IBaseEntity{
  productName: string,
  productInCategoryId: number,
  productWithDefectId: number,
  productReturnedId: number,
  productSoldId: number
}
