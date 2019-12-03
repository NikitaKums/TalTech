import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ILogin} from "../interfaces/ILogin";
import {IdentityService} from "../services/identity-service";
import {AppConfig} from "../app-config";

export var log = LogManager.getLogger('Identity.Login');

@autoinject
export class Login {

  private login: ILogin;

  constructor(
    private identityService: IdentityService,
    private appConfig: AppConfig,
    private router: Router) {
    log.debug('constructor');
  }

  // ============ View methods ==============
  submit(): void {
    log.debug("submit", this.login.email, this.login.password);
    this.identityService.login(this.login).then(jwtDTO => {
      if (jwtDTO.token !== undefined) {
        log.debug("submit token", jwtDTO.token);
        this.appConfig.jwt = jwtDTO.token;
        this.router.navigateToRoute('home');
      } else {
        alert("Unsuccessful login attempt.")
      }
    });
  }


  // ============ View LifeCycle events ==============
  created(owningView: View, myView: View) {
    log.debug('created');
  }

  bind(bindingContext: Object, overrideContext: Object) {
    log.debug('bind');
  }

  attached() {
    log.debug('attached');
  }

  detached() {
    log.debug('detached');
  }

  unbind() {
    log.debug('unbind');
  }

  // ============= Router Events =============
  canActivate(params: any, routerConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    log.debug('canActivate');
  }

  activate(params: any, routerConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    log.debug('activate');
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
