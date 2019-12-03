import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {DrinksInOrderService} from "../services/drinks-in-order-service";
import {IDrinkInOrder} from "../interfaces/IDrinkInOrder";
import {IDrink} from "../interfaces/IDrink";
import {IOrder} from "../interfaces/IOrder";
import {DrinksService} from "../services/drinks-service";
import {OrdersService} from "../services/orders-service";

export var log = LogManager.getLogger('Comments.Edit');

@autoinject
export class Edit {

  private drinkInOrder: IDrinkInOrder;
  private drinks: IDrink[] = [];
  private order: IOrder;
  private selectedField: number;
  
  constructor(private router: Router,
              private drinksInOrderService: DrinksInOrderService,
              private drinksService: DrinksService,
              private ordersService: OrdersService) {
    log.debug('constructor');
  }
  // ============ View methods ==============
  submit():void{
    log.debug('drinkInOrder', this.drinkInOrder);
    this.drinksInOrderService.put(this.drinkInOrder!).then(
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
    this.drinksService.fetchAll(undefined).then(
      jsonData => {
        log.debug("drinks", jsonData);
        this.drinks = jsonData;
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
    this.drinksInOrderService.fetch(params.id).then(
      drinkInOrder => {
        log.debug('drinkInOrder', drinkInOrder);
        this.drinkInOrder = drinkInOrder;
        this.selectedField = this.drinkInOrder.orderId;
        this.ordersService.fetch(this.selectedField).then(
          jsonData => {
            log.debug("order", jsonData);
            this.order = jsonData;
          }
        );
      });
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
