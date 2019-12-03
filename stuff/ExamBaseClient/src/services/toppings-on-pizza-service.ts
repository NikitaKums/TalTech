import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IToppingOnPizza} from "../interfaces/IToppingOnPizza";

export var log = LogManager.getLogger('ToppingsOnPizzaService');

@autoinject
export class ToppingsOnPizzaService extends BaseService<IToppingOnPizza> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'ToppingsOnPizza');
  }
}
