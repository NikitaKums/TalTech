import {IBaseEntity} from "./IBaseEntity";

export interface IDrinkInOrder extends IBaseEntity{
  orderId: number,
  orderDescription: string,
  drinkId: number,
  drinkDescription: string
}
