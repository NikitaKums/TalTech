import {IBaseEntity} from "./IBaseEntity";

export interface IInventory extends IBaseEntity{
  description: string,
  shopId: number,
  shopName: number,
  productCount: number,
  inventoryCreationTime: Date,
  dateString: string
}
