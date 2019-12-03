import {IBaseEntity} from "./IBaseEntity";

export interface IDefect extends IBaseEntity{
  description: string,
  shopId: number,
  shopName: number,
  productsWithDefectCount: number
}
