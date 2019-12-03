import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IOrder} from "../interfaces/IOrder";
import {OrdersService} from "../services/orders-service";
import {IShipper} from "../interfaces/IShipper";
import {ShippersService} from "../services/shippers-service";

export var log = LogManager.getLogger('Orders.Edit');

@autoinject
export class Edit {

  private order: IOrder;
  private shippers: IShipper[] = [];

  constructor(private router: Router,
              private ordersService: OrdersService,
              private shippersService: ShippersService) {
    log.debug('constructor');
  }


  // ============ View Methods ============
  submit():void{
    log.debug('order', this.order);
    try {
      this.order.orderCreationTime.setMinutes(this.order.orderCreationTime.getMinutes() + 180);
    } catch (e) {
    }
    this.ordersService.put(this.order).then(
      response => {
        if (response.status == 204){
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

    this.shippersService.fetchAll(undefined, undefined, undefined).then(
      shipper => {
        log.debug('shipper', shipper);
        this.shippers = shipper;
      });
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
