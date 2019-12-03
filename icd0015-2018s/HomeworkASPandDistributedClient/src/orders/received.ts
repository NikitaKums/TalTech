import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {OrdersService} from "../services/orders-service";
import {IOrderReceived} from "../interfaces/IOrderReceived";
import {ProductsInOrderService} from "../services/products-in-order-service";

export var log = LogManager.getLogger('Orders.Received');

@autoinject
export class Received {

  private ordersReceived: IOrderReceived[] = [];
  private receivedOrderId: number;

  constructor(private router: Router,
              private ordersService: OrdersService,
              private productsInOrderService: ProductsInOrderService) {
    log.debug('constructor');
  }


  // ============ View Methods ============
  submit():void{
    log.debug('order');

    this.ordersService.allOrdersReceived(this.receivedOrderId).then(
      response => {
        if (response.status == 200){
          this.router.navigateToRoute("ordersIndex");
        } else {
          alert("An error occured!");
          log.error('Error in response! ', response);
        }
      }
    );
  }
  
  submitSingle(id: number):void {
    log.debug('submit single');
    this.productsInOrderService.orderReceived(id).then(
      response => {
        if (response.status == 200){
          this.router.navigateToRoute("ordersReceived", {id: this.receivedOrderId});
        } else {
          alert("An error occured!");
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
    this.receivedOrderId = params.id;
    this.ordersService.fetchForOrdersReceived(params.id).then(
      jsonData => {
        log.debug('jsonData', jsonData);
        this.ordersReceived = jsonData;
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
