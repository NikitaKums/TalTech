import {IBaseEntity} from "./IBaseEntity";

export interface IToppingOnPizza extends IBaseEntity{
  pizzaId: number,
  pizzaDescription: string,
  toppingId: number,
  toppingDescription: string
}
