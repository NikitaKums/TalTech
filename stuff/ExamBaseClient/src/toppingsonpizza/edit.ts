import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ToppingsOnPizzaService} from "../services/toppings-on-pizza-service";
import {IToppingOnPizza} from "../interfaces/IToppingOnPizza";
import {ITopping} from "../interfaces/ITopping";
import {IPizza} from "../interfaces/IPizza";
import {ToppingsService} from "../services/toppings-service";
import {PizzasService} from "../services/pizzas-service";

export var log = LogManager.getLogger('toppingonpizzas.Edit');

@autoinject
export class Edit {

  private toppingOnPizza: IToppingOnPizza;
  private toppings: ITopping[] = [];
  private pizza: IPizza;
  private selectedField: number;

  constructor(private router: Router,
              private toppingsOnPizzaService: ToppingsOnPizzaService,
              private toppingsService: ToppingsService,
              private pizzasService: PizzasService) {
    log.debug('constructor');
  }
  // ============ View methods ==============
  submit():void{
    log.debug('toppingonpizza', this.toppingOnPizza);
    this.toppingsOnPizzaService.put(this.toppingOnPizza!).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("pizzasIndex");
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
    this.toppingsService.fetchAll(undefined).then(
      jsonData => {
        log.debug("toppings", jsonData);
        this.toppings = jsonData;
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
    this.toppingsOnPizzaService.fetch(params.id).then(
      toppingOnPizza => {
        log.debug('toppingOnPizza', toppingOnPizza);
        this.toppingOnPizza = toppingOnPizza;
        this.selectedField = this.toppingOnPizza.pizzaId;
        this.pizzasService.fetch(this.selectedField).then(
          jsonData => {
            log.debug("order", jsonData);
            this.pizza = jsonData;
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
