import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IProductInOrder} from "../interfaces/IProductInOrder";
import {IIdAndNameProduct} from "../interfaces/IIdAndNameProduct";

export var log = LogManager.getLogger('ProductsInOrderService');

@autoinject
export class ProductsInOrderService extends BaseService<IProductInOrder> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'ProductsInOrder');
  }

  orderReceived(id: number): Promise<Response> {
    let url = this.appConfig.apiUrl + "ProductsInOrder/ProductInOrderReceived/" + id;

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

  fetchForInfoByOrderId(orderId: number): Promise<IProductInOrder[]> {

    let url = this.appConfig.apiUrl + "ProductsInOrder/GetInfoByOrderId" + "/" + orderId;

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
