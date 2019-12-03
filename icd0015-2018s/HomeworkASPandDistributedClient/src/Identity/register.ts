import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IRegister} from "../interfaces/IRegister";
import {IdentityService} from "../services/identity-service";
import {AppConfig} from "../app-config";

export var log = LogManager.getLogger('Identity.Register');

@autoinject
export class Register {
  
  private register: IRegister;

  constructor(
    private identityService: IdentityService,
    private appConfig: AppConfig,
    private router: Router
  ) {
    log.debug('constructor');
  }

  // ============ View methods ==============
  submit(): void{
    log.debug("submit", this.register.email, this.register.password, this.register.confirmPassword, this.register.lastName, this.register.firstName);
    
    if (this.register.password == null || this.register.confirmPassword == null || this.register.email == null ||
      this.register.password != this.register.confirmPassword || this.register.password.length < 6 ||
      this.register.email.length == 0){
      alert('Passwords dont match or password too short or username too short!');
      return;
    }
    
    this.identityService.register(this.register)
      .then(jwtDTO => {
        if (jwtDTO.token !== undefined) {
          log.debug("submit token", jwtDTO.token);
          this.appConfig.jwt = jwtDTO.token;
          this.router.navigateToRoute('home');
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
