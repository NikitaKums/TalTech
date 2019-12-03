import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IDefect} from "../interfaces/IDefect";

export var log = LogManager.getLogger('DefectsService');

@autoinject
export class DefectsService extends BaseService<IDefect> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'Defects');
  }

}
