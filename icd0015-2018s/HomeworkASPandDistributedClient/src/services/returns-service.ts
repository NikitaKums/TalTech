import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IReturn} from "../interfaces/IReturn";
import {IIdAndDescReturn} from "../interfaces/IIdAndDescReturn";

export var log = LogManager.getLogger('ReturnsService');

@autoinject
export class ReturnsService extends BaseService<IReturn> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'Returns');
  }
  
  fetchForReturnIdAndName(): Promise<IIdAndDescReturn[]> {

    let url = this.appConfig.apiUrl + "Returns/ReturnIdName";

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
