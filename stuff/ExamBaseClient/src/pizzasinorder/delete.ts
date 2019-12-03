import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IPizzaInOrder} from "../interfaces/IPizzaInOrder";
import {PizzasInOrderService} from "../services/pizzas-in-order-service";

export var log = LogManager.getLogger('pizzaInOrders.Delete');

@autoinject
export class Delete {

  private pizzaInOrder: IPizzaInOrder;

  constructor(private router: Router,
              private pizzasInOrderService: PizzasInOrderService) {
    log.debug('constructor');
  }

  // ============ View Methods ================
  submit():void{
    log.debug('submit', this.pizzaInOrder);

    this.pizzasInOrderService.delete(this.pizzaInOrder.id).then(
      response => {
        if (response.status == 200){
          this.router.navigateToRoute("pizzainordersIndex");
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
    this.pizzasInOrderService.fetch(params.id).then(
      pizzaInOrder => {
        log.debug("pizzaInOrder", pizzaInOrder);
        this.pizzaInOrder = pizzaInOrder;
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
