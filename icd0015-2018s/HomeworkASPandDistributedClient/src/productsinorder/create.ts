import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ProductsService} from "../services/products-service";
import {IIdAndNameProduct} from "../interfaces/IIdAndNameProduct";
import {IOrder} from "../interfaces/IOrder";
import {OrdersService} from "../services/orders-service";
import {ProductsInOrderService} from "../services/products-in-order-service";
import {IProductInOrder} from "../interfaces/IProductInOrder";

export var log = LogManager.getLogger('ProductsInOrder.Create');

@autoinject
export class Create {

  private productInOrder: IProductInOrder;
  private orders: IOrder[] = [];
  private products: IIdAndNameProduct[] = [];
  private selectedField: number;

  constructor(private router: Router,
              private ordersService: OrdersService,
              private productsService: ProductsService,
              private productsInOrderService: ProductsInOrderService) {
    log.debug('constructor');
  }

  // ============ View Methods ============
  submit():void{
    log.debug('productInOrder', this.productInOrder);
    this.productInOrder.productInOrderPlacingTime.setMinutes(this.productInOrder.productInOrderPlacingTime.getMinutes() + 180);
    this.productInOrder.id = 0;
    this.productInOrder.orderId = this.selectedField;
    this.productsInOrderService.post(this.productInOrder).then(
      response => {
        if (response.status == 201){
          this.router.navigateToRoute("ordersIndex");
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
    this.selectedField = params.id;
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
