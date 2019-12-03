import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IProductReturned} from "../interfaces/IProductReturned";
import {ProductsReturnedService} from "../services/products-returned-service";

export var log = LogManager.getLogger('ProductsReturned.Index');

@autoinject
export class Index {

  private productsReturned: IProductReturned[] = [];

  constructor(private router: Router,
              private productsReturnedService: ProductsReturnedService) {
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
    /*this.productsReturnedService.fetchAll().then(
      jsonData => {
        log.debug('jsonData', jsonData);
        this.productsReturned = jsonData;
        for (let productReturned of this.productsReturned){
          productReturned .dateString = productReturned.productReturnedTime.toString().replace("T", " ");
        }
      }
    );*/
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
