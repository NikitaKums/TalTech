import {IBaseEntity} from "./IBaseEntity";

export interface IAppUser extends IBaseEntity{
  firstName: string,
  lastName: string,
  address: string,
  shopId: number,
  shopName: string
}
