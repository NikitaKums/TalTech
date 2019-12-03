import {IBaseEntity} from "./IBaseEntity";

export interface IShipper extends IBaseEntity{
  shipperName: string,
  shipperAddress: string,
  phoneNumber: string,
  ordersCount: number
}
