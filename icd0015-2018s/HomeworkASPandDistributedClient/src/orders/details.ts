import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IOrder} from "../interfaces/IOrder";
import {OrdersService} from "../services/orders-service";
import {ProductsInOrderService} from "../services/products-in-order-service";
import {IProductInOrder} from "../interfaces/IProductInOrder";

export var log = LogManager.getLogger('Orders.Details');

@autoinject
export class Details {

  private order: IOrder;
  private productsInOrder: IProductInOrder[] = [];

  constructor(private router: Router,
              private ordersService: OrdersService,
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
    this.ordersService.fetch(params.id).then(
      order => {
        log.debug("order", order);
        this.order = order;
        this.order.dateString = this.order.orderCreationTime.toString().replace("T", " ");
      }
    );

    this.productsInOrderService.fetchForInfoByOrderId(params.id).then(
      productInOrder => {
        log.debug("productInOrder", productInOrder);
        this.productsInOrder = productInOrder;
        for (let productInOrder of this.productsInOrder){
            productInOrder.dateString = productInOrder.productInOrderPlacingTime.toString().replace("T", " ");
        } 
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
