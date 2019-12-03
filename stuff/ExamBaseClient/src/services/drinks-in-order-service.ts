import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IComment} from "../interfaces/IComment";
import {IDrinkInOrder} from "../interfaces/IDrinkInOrder";

export var log = LogManager.getLogger('DrinksInOrderService');

@autoinject
export class DrinksInOrderService extends BaseService<IDrinkInOrder> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'DrinksInOrder');
  }
}
