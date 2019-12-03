import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IProductInOrder} from "../interfaces/IProductInOrder";
import {ProductsInOrderService} from "../services/products-in-order-service";

export var log = LogManager.getLogger('ProductsInOrder.Delete');

@autoinject
export class Delete {

  private productInOrder: IProductInOrder;

  constructor(private router: Router,
              private productsInOrderService: ProductsInOrderService) {
    log.debug('constructor');
  }

  // ============ View Methods ================
  submit(): void {
    log.debug('submit', this.productInOrder);

    this.productsInOrderService.delete(this.productInOrder.id).then(
      response => {
        if (response.status == 200) {
          this.router.navigateToRoute("ordersDetails", {id: this.productInOrder.orderId});
        } else {
          log.debug("response", response.status);
        }
      }
    );

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
    this.productsInOrderService.fetch(params.id)
      .then(
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
