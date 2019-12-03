import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {PizzasInOrderService} from "../services/pizzas-in-order-service";
import {IPizzaInOrder} from "../interfaces/IPizzaInOrder";

export var log = LogManager.getLogger('pizzaInOrders.Edit');

@autoinject
export class Edit {

  private pizzaInOrder: IPizzaInOrder;

  constructor(private router: Router,
              private pizzasInOrderService: PizzasInOrderService) {
    log.debug('constructor');
  }
  // ============ View methods ==============
  submit():void{
    log.debug('pizzaInOrder', this.pizzaInOrder);
    this.pizzasInOrderService.put(this.pizzaInOrder!).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("pizzainordersIndex");
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
    this.pizzasInOrderService.fetch(params.id).then(
      pizzaInOrder => {
        log.debug('pizzaInOrder', pizzaInOrder);
        this.pizzaInOrder = pizzaInOrder;
      });
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
