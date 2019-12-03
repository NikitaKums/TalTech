import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IDrink} from "../interfaces/IDrink";

export var log = LogManager.getLogger('DrinksService');

@autoinject
export class DrinksService extends BaseService<IDrink> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'Drinks');
  }
}
