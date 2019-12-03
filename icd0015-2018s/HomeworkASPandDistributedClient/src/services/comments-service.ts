import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IComment} from "../interfaces/IComment";

export var log = LogManager.getLogger('CommentsService');

@autoinject
export class CommentsService extends BaseService<IComment> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'Comments');
  }

}
