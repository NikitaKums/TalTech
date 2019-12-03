import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IProductReturned} from "../interfaces/IProductReturned";

export var log = LogManager.getLogger('ProductReturnedService');

@autoinject
export class ProductsReturnedService extends BaseService<IProductReturned> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'ProductsReturned');
  }

  fetchForInfoByReturnId(returnId: number): Promise<IProductReturned[]> {

    let url = this.appConfig.apiUrl + "ProductsReturned/GetInfoByReturnId" + "/" + returnId;

    return this.httpClient.fetch(url,
      {
        cache: 'no-store',
        headers: {
          Authorization: 'Bearer ' + this.appConfig.jwt,
        }
      })
      .then(response => {
        log.debug('response', response);
        return response.json();
      })
      .then(jsonData => {
        log.debug('jsonData', jsonData);
        return jsonData;
      }).catch(reason => {
        log.debug('catch reason', reason);
      });
  }
}
