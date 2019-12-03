import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IProductSold} from "../interfaces/IProductSold";
import {IProductInOrder} from "../interfaces/IProductInOrder";

export var log = LogManager.getLogger('ProductsSoldService');

@autoinject
export class ProductsSoldService extends BaseService<IProductSold> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'ProductsSold');
  }

  deleteWithRestore(id: number): Promise<Response> {
    let url = this.appConfig.apiUrl + "ProductsSold/DeleteWithProductRestore/" + id;

    return this.httpClient.post(url, null, {
      cache: 'no-store',
      headers: {
        Authorization: 'Bearer ' + this.appConfig.jwt,
      }
    }).then(
      response => {
        log.debug('response', response);
        return response;
      }
    );
  }

  fetchForInfoBySaleId(saleId: number): Promise<IProductSold[]> {

    let url = this.appConfig.apiUrl + "ProductsSold/GetInfoBySaleId" + "/" + saleId;

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

