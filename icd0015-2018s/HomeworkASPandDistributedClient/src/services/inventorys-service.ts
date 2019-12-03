import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IInventory} from "../interfaces/IInventory";

export var log = LogManager.getLogger('InventorysService');

@autoinject
export class InventorysService extends BaseService<IInventory> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'Inventorys');
  }

}
