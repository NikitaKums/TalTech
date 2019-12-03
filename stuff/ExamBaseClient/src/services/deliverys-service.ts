import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IDelivery} from "../interfaces/IDelivery";

export var log = LogManager.getLogger('DeliverysService');

@autoinject
export class DeliverysService extends BaseService<IDelivery> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'Deliverys');
  }
}
