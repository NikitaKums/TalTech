import {IBaseEntity} from "./IBaseEntity";
import {ITopping} from "./ITopping";
import {IToppingOnPizza} from "./IToppingOnPizza";

export interface IPizza extends IBaseEntity{
  description: string,
  pirce: number,
  toppingsOnPizza: [IToppingOnPizza]
}
