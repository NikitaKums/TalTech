import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {ISale} from "../interfaces/ISale";
import {IIdAndDescSale} from "../interfaces/IIdAndDescSale";

export var log = LogManager.getLogger('SalesService');

@autoinject
export class SalesService extends BaseService<ISale> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'Sales');
  }
  
  getTotalSaleAmounts(): Promise<{ [id: string] : number|undefined; }> {

    let url = this.appConfig.apiUrl + "Sales/GetTotalSaleAmounts";

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

  fetchForSaleIdAndName(): Promise<IIdAndDescSale[]> {

    let url = this.appConfig.apiUrl + "Sales/GetSalesForUser";

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
