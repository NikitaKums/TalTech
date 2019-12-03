import {IBaseEntity} from "./IBaseEntity";

export interface IManuFacturer extends IBaseEntity{
  manuFacturerName: string,
  aadress: string,
  phoneNumber: string,
  productCount: number
}
