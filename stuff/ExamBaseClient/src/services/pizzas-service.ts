import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IPizza} from "../interfaces/IPizza";

export var log = LogManager.getLogger('PizzasService');

@autoinject
export class PizzasService extends BaseService<IPizza> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'Pizzas');
  }
}
