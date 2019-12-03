import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IPizzaInOrder} from "../interfaces/IPizzaInOrder";

export var log = LogManager.getLogger('CommentsService');

@autoinject
export class PizzasInOrderService extends BaseService<IPizzaInOrder> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'PizzasInOrder');
  }
}
