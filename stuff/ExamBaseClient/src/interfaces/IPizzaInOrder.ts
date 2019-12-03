import {IBaseEntity} from "./IBaseEntity";

export interface IPizzaInOrder extends IBaseEntity{
  orderId: number,
  orderDescription: string,
  pizzaId: number,
  pizzaDescription: string
}
