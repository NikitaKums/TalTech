import {IBaseEntity} from "./IBaseEntity";

export interface IProductInCategory extends IBaseEntity{
  categoryId: number,
  categoryName: string,
  
  shopId: number,
  shopName: string
}
