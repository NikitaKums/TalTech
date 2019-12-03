import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IOrder} from "../interfaces/IOrder";
import {OrdersService} from "../services/orders-service";
import {IShop} from "../interfaces/IShop";
import {IShipper} from "../interfaces/IShipper";
import {ShippersService} from "../services/shippers-service";

export var log = LogManager.getLogger('Orders.Create');

@autoinject
export class Create {

  private order: IOrder;
  private shop: IShop;
  private shippers: IShipper[] = [];

  constructor(private router: Router,
              private ordersService: OrdersService,
              private shippersService: ShippersService) {
    log.debug('constructor');
  }


  // ============ View Methods ============
  submit():void{
    log.debug('order', this.order); 
    this.order.orderCreationTime.setMinutes(this.order.orderCreationTime.getMinutes() + 180);
    this.order.shopId = this.shop.id;
    this.ordersService.post(this.order).then(
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
    this.ordersService.fetchForShop().then(
      jsonData => {
        log.debug("shop", jsonData);
        this.shop = jsonData;
      }
    );
    
    this.shippersService.fetchAll(undefined, undefined, undefined).then(
      jsonData => {
        log.debug("shop", jsonData);
        this.shippers = jsonData;
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
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
