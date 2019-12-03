import {IBaseEntity} from "./IBaseEntity";
import {IProductSoldProductSaleIdName} from "./IProductSoldProductSaleIdName";

export interface ISale extends IBaseEntity {
  description: string,
  saleInitialCreationTime: Date,
  productsSoldCount: number,
  appUserId: number,
  productsInSaleDTOs: IProductSoldProductSaleIdName[],
  dateString: string,
  appUserName: string,
  appUserLastName: string,
  todayTotalSaleAmount: number | undefined,
  allTotalSaleAmount: number | undefined
}
