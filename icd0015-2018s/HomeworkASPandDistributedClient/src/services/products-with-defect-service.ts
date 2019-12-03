import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IProductWithDefect} from "../interfaces/IProductWithDefect";

export var log = LogManager.getLogger('ProductsWithDefectService');

@autoinject
export class ProductsWithDefectService extends BaseService<IProductWithDefect> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'ProductsWithDefect');
  }

  fetchForInfoByDefectId(defectId: number): Promise<IProductWithDefect[]> {

    let url = this.appConfig.apiUrl + "ProductsWithDefect/GetInfoByDefectId" + "/" + defectId;

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
