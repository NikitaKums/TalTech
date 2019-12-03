import {IBaseEntity} from "./IBaseEntity";
import {IPizzaInOrder} from "./IPizzaInOrder";
import {IDrinkInOrder} from "./IDrinkInOrder";

export interface IOrder extends IBaseEntity{
  description: string,
  price: number,
  orderState: string,
  deliveryId: number,
  deliveryService: string,
  pizzasInOrder: [IPizzaInOrder],
  drinksInOrder: [IDrinkInOrder]
}
