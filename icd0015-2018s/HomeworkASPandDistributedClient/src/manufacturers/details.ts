import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ManufacturersService} from "../services/manufacturers-service";
import {IManuFacturer} from "../interfaces/IManuFacturer";

export var log = LogManager.getLogger('ManuFacturers.Details');

@autoinject
export class Details {
  
  private manuFacturer: IManuFacturer | null = null;
  
  constructor(private router: Router,
    private manuFacturersService: ManufacturersService) {
    log.debug('constructor');
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
    this.manuFacturersService.fetch(params.id).then(
      manuFacturer => {
        log.debug("manuFacturer", manuFacturer);
        this.manuFacturer = manuFacturer;
      }
    );
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
