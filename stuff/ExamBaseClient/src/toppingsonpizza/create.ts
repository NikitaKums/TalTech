import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IToppingOnPizza} from "../interfaces/IToppingOnPizza";
import {ToppingsOnPizzaService} from "../services/toppings-on-pizza-service";
import {IPizza} from "../interfaces/IPizza";
import {ITopping} from "../interfaces/ITopping";
import {ToppingsService} from "../services/toppings-service";
import {PizzasService} from "../services/pizzas-service";

export var log = LogManager.getLogger('toppingonpizzas.Create');

@autoinject
export class Create {

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

  // ============ View Methods ============
  submit():void{
    log.debug('toppingonpizza', this.toppingOnPizza);
    this.toppingOnPizza.id = 0;
    this.toppingOnPizza.pizzaId = this.selectedField;
    this.toppingsOnPizzaService.post(this.toppingOnPizza).then(
      response => {
        if (response.status == 201){
          this.router.navigateToRoute("pizzasIndex");
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
    this.toppingsService.fetchAll(undefined).then(
      jsonData => {
        log.debug("toppings", jsonData);
        this.toppings = jsonData;
      }
    );
    this.pizzasService.fetch(this.selectedField).then(
      jsonData => {
        log.debug("order", jsonData);
        this.pizza = jsonData;
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
