import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IProductInOrder} from "../interfaces/IProductInOrder";
import {ProductsInOrderService} from "../services/products-in-order-service";

export var log = LogManager.getLogger('ProductsInOrder.Details');

@autoinject
export class Details {

  private productInOrder: IProductInOrder;

  constructor(private router: Router,
              private productsInOrderService: ProductsInOrderService) {
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
    this.productsInOrderService.fetch(params.id).then(
      jsonData => {
        log.debug('jsonData', jsonData);
        this.productInOrder = jsonData;
        this.productInOrder.dateString = this.productInOrder.productInOrderPlacingTime.toString().replace("T", " ");
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
