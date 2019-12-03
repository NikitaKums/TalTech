import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IPizzaInOrder} from "../interfaces/IPizzaInOrder";
import {PizzasInOrderService} from "../services/pizzas-in-order-service";
import {OrdersService} from "../services/orders-service";
import {PizzasService} from "../services/pizzas-service";
import {IOrder} from "../interfaces/IOrder";
import {IPizza} from "../interfaces/IPizza";

export var log = LogManager.getLogger('pizzaInOrders.Create');

@autoinject
export class Create {

  private pizzaInOrder: IPizzaInOrder;
  private pizzas: IPizza[] = [];
  private order: IOrder;
  private selectedField: number;

  constructor(private router: Router,
              private pizzasInOrderService: PizzasInOrderService,
              private pizzasService: PizzasService,
              private ordersService: OrdersService) {
    log.debug('constructor');
  }

  // ============ View Methods ============
  submit():void{
    log.debug('pizzaInOrder', this.pizzaInOrder);
    this.pizzaInOrder.id = 0;
    this.pizzaInOrder.orderId = this.selectedField;
    this.pizzasInOrderService.post(this.pizzaInOrder).then(
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
    this.pizzasService.fetchAll(undefined).then(
      jsonData => {
        log.debug("pizzas", jsonData);
        this.pizzas = jsonData;
      }
    );
    this.ordersService.fetch(this.selectedField).then(
      jsonData => {
        log.debug("order", jsonData);
        this.order = jsonData;
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
    this.selectedField = params.id;
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
