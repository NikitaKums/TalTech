import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IOrder} from "../interfaces/IOrder";

export var log = LogManager.getLogger('OrdersService');

@autoinject
export class OrdersService extends BaseService<IOrder> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'Orders');
  }
}
