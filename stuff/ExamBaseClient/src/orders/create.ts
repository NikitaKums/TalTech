import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {OrdersService} from "../services/orders-service";
import {IOrder} from "../interfaces/IOrder";
import {IDelivery} from "../interfaces/IDelivery";
import {DeliverysService} from "../services/deliverys-service";

export var log = LogManager.getLogger('Comments.Create');

@autoinject
export class Create {

  private order: IOrder;
  private deliverys: IDelivery[] = [];

  constructor(private router: Router,
              private ordersService: OrdersService,
              private deliveryService: DeliverysService) {
    log.debug('constructor');
  }

  // ============ View Methods ============
  submit():void{
    log.debug('order', this.order);
    this.ordersService.post(this.order).then(
      response => {
        if (response.status == 201){
          this.router.navigateToRoute("ordersIndex");
        } else {
          alert("Invalid data entered");
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
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
