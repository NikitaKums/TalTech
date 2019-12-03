import {IBaseEntity} from "./IBaseEntity";
import {IOrder} from "./IOrder";

export interface IDelivery extends IBaseEntity{
  deliveryService: string,
  description: string,
  deliveryPrice: number,
  ordersInDelivery:[IOrder]
}
