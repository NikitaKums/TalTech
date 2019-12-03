import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from 'aurelia-fetch-client';
import {AppConfig} from "../app-config";
import {IRegister} from "../interfaces/IRegister";
import {ILogin} from "../interfaces/ILogin";

export var log = LogManager.getLogger('IdentityService');

@autoinject()
export class IdentityService {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig,
    private endPoint: string) {
    log.debug('constructor');
  }
  
  login(loginModel: ILogin):Promise<any>{
    let url = this.appConfig.apiUrl + "account/login";
    let loginDTO = {
      email: loginModel.email,
      password: loginModel.password
    };
    
    return this.httpClient.post(url, JSON.stringify(loginDTO), { cache: 'no-store' }).then(
      response => {
        log.debug('response', response);
        return response.json();
      }
    ).catch(
      reason => {
        log.debug('catch reason', reason);
      }
    );

  }
  
  register(registerModel: IRegister):Promise<any>{
    let url = this.appConfig.apiUrl + "account/register";
    let registerDTO = {
      email: registerModel.email,
      password: registerModel.password,
      firstName: registerModel.firstName,
      lastName: registerModel.lastName
    };

    return this.httpClient.post(url, JSON.stringify(registerDTO), { cache: 'no-store' }).then(
      response => {
        log.debug('response', response);
        return response.json();
      }
    ).catch(
      reason => {
        log.debug('catch reason', reason);
      }
    );

  }
}
