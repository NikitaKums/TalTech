import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IIdAndNameProduct} from "../interfaces/IIdAndNameProduct";
import {ProductsService} from "../services/products-service";
import {IProductInOrder} from "../interfaces/IProductInOrder";
import {IOrder} from "../interfaces/IOrder";
import {OrdersService} from "../services/orders-service";
import {ProductsInOrderService} from "../services/products-in-order-service";

export var log = LogManager.getLogger('ProductsInOrder.Edit');

@autoinject
export class Edit {

  private productInOrder: IProductInOrder;
  private orders: IOrder[] = [];
  private products: IIdAndNameProduct[] = [];
  private returnOrderId: number;

  constructor(private router: Router,
              private ordersService: OrdersService,
              private productsService: ProductsService,
              private productsInOrderService: ProductsInOrderService) {
    log.debug('constructor');
  }

  // ============ View Methods ============
  submit(): void {
    log.debug('productInOrder', this.productInOrder);
    try {
      this.productInOrder.productInOrderPlacingTime.setMinutes(this.productInOrder.productInOrderPlacingTime.getMinutes() + 180);
    } catch (e) {
    }
    this.productsInOrderService.put(this.productInOrder).then(
      response => {
        if (response.status == 204) {
          this.router.navigateToRoute("ordersDetails", {id: this.returnOrderId});
        } else {
          alert("Invalid data entered!");
          log.error('Error in response! ', response);
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
    this.productsInOrderService.fetch(params.id).then(
      productInOrder => {
        log.debug('productInOrder', productInOrder);
        this.productInOrder = productInOrder;
        this.returnOrderId = this.productInOrder.orderId;
      }
    );

    this.productsService.fetchForProductsIdAndName().then(
      jsonData => {
        log.debug("products", jsonData);
        this.products = jsonData;
      }
    );

    this.ordersService.fetchAll(undefined, undefined, undefined).then(
      jsonData => {
        log.debug("orders", jsonData);
        this.orders = jsonData;
      }
    )
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
