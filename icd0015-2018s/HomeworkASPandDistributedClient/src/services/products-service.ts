import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IIdAndNameProduct} from "../interfaces/IIdAndNameProduct";
import {IProduct} from "../interfaces/IProduct";

export var log = LogManager.getLogger('ProductsService');

@autoinject
export class ProductsService extends BaseService<IProduct> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'Products');
  }

  fetchForProductsIdAndName(): Promise<IIdAndNameProduct[]> {

    let url = this.appConfig.apiUrl + "Products/GetIdAndName";

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

  fetchForProductsIdAndNameByCategoryId(categoryId: number): Promise<IIdAndNameProduct[]> {

    let url = this.appConfig.apiUrl + "Products/GetIdAndNameByCategory" + "/" + categoryId;

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

  fetchForProductsIdAndNameByDefectId(defectId: number): Promise<IIdAndNameProduct[]> {

    let url = this.appConfig.apiUrl + "Products/GetIdAndNameByDefect" + "/" + defectId;

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
