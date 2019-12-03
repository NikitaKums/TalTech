import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IOrder} from "../interfaces/IOrder";
import {IIdAndNameProduct} from "../interfaces/IIdAndNameProduct";
import {IOrderReceived} from "../interfaces/IOrderReceived";

export var log = LogManager.getLogger('OrdersService');

@autoinject
export class OrdersService extends BaseService<IOrder> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'Orders');
  }

  fetchForOrdersReceived(orderId: number): Promise<IOrderReceived[]> {

    let url = this.appConfig.apiUrl + "Orders/GetOrderReceived/" + orderId;

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

  allOrdersReceived(orderId: number): Promise<Response> {
    let url = this.appConfig.apiUrl + "Orders/AllOrdersReceived/" + orderId;

    return this.httpClient.post(url, null,{
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

}
