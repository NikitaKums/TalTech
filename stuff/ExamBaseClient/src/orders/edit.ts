import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IOrder} from "../interfaces/IOrder";
import {OrdersService} from "../services/orders-service";
import {IDelivery} from "../interfaces/IDelivery";
import {DeliverysService} from "../services/deliverys-service";

export var log = LogManager.getLogger('Comments.Edit');

@autoinject
export class Edit {

  private order: IOrder;
  private deliverys: IDelivery[] = [];

  constructor(private router: Router,
              private ordersService: OrdersService,
              private deliveryService: DeliverysService) {
    log.debug('constructor');
  }
  // ============ View methods ==============
  submit():void{
    log.debug('orders', this.order);
    this.ordersService.put(this.order!).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("ordersIndex");
        } else {
          alert("Invalid data entered");
          log.error('Error in response!', response);
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
    this.deliveryService.fetchAll(undefined).then(
      jsonData => {
        log.debug("order", jsonData);
        this.deliverys = jsonData;
      }
    );
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
        log.debug('order', order);
        this.order = order;
      });
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
