import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IOrder} from "../interfaces/IOrder";
import {OrdersService} from "../services/orders-service";

export var log = LogManager.getLogger('Comments.Delete');

@autoinject
export class Delete {

  private order: IOrder;

  constructor(private router: Router,
              private ordersService: OrdersService) {
    log.debug('constructor');
  }

  // ============ View Methods ================
  submit():void{
    log.debug('submit', this.order);

    this.ordersService.delete(this.order.id).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("ordersIndex");
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
    this.ordersService.fetch(params.id).then(
      order => {
        log.debug("order", order);
        this.order = order;
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
