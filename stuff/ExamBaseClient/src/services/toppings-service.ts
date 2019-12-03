import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {ITopping} from "../interfaces/ITopping";

export var log = LogManager.getLogger('ToppingsService');

@autoinject
export class ToppingsService extends BaseService<ITopping> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'Toppings');
  }
}
