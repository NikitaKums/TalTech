import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IShop} from "../interfaces/IShop";

export var log = LogManager.getLogger('ShopsService');

@autoinject
export class ShopsService extends BaseService<IShop> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'Shops');
  }

}
