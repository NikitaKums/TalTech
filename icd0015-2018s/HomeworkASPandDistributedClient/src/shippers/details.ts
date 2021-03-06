import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ShippersService} from "../services/shippers-service";
import {IShipper} from "../interfaces/IShipper";

export var log = LogManager.getLogger('Shippers.Details');

@autoinject
export class Details {
  
  private shipper: IShipper | null = null;
  
  constructor(private router: Router,
    private shippersService: ShippersService) {
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
    this.shippersService.fetch(params.id).then(
      shipper => {
        log.debug("shipper", shipper);
        this.shipper = shipper;
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
