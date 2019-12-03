import {IBaseEntity} from "./IBaseEntity";

export interface IShop extends IBaseEntity{
  shopName: string,
  shopAddress: string,
  shopContact: string,
  shopContact2: string,
  inventoryId: number,
  ordersCount: number,
  productsCount: number,
  returnsCount: number,
  defectsCount: number,
  appUsersCount: number,
}
