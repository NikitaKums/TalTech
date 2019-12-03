import {IBaseEntity} from "./IBaseEntity";
import {IIdAndNameProduct} from "./IIdAndNameProduct";

export interface IReturn extends IBaseEntity{
  description: string,
  shopId: number,
  shopName: string,
  productsReturnedCount: number,
  productIdNameDtos: IIdAndNameProduct []
}
