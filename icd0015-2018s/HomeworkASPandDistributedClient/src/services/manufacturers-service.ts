import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IManuFacturer} from "../interfaces/IManuFacturer";

export var log = LogManager.getLogger('ManuFacturersService');

@autoinject
export class ManufacturersService extends BaseService<IManuFacturer> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'ManuFacturers');
  }

}
