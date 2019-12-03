import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IProductInCategory} from "../interfaces/IProductInCategory";

export var log = LogManager.getLogger('ProductsInCategoryService');

@autoinject
export class ProductsInCategoryService extends BaseService<IProductInCategory> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'ProductsInCategory');
  }
}
