import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IShipper} from "../interfaces/IShipper";

export var log = LogManager.getLogger('ShippersService');

@autoinject
export class ShippersService extends BaseService<IShipper> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'Shippers');
  }

}
