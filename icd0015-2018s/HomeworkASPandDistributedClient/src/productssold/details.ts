import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IProductSold} from "../interfaces/IProductSold";
import {ProductsSoldService} from "../services/products-sold-service";

export var log = LogManager.getLogger('ProductsSold.Details');

@autoinject
export class Details {

  private productsSold: IProductSold;

  constructor(private router: Router,
              private productsSoldService: ProductsSoldService) {
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
    this.productsSoldService.fetch(params.id).then(
      jsonData => {
        log.debug('jsonData', jsonData);
        this.productsSold = jsonData;
        this.productsSold.dateString = this.productsSold.productSoldTime.toString().replace("T", " ");
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
