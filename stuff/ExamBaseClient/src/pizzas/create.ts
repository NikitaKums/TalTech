import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {PizzasService} from "../services/pizzas-service";
import {IPizzaInOrder} from "../interfaces/IPizzaInOrder";
import {IPizza} from "../interfaces/IPizza";

export var log = LogManager.getLogger('pizzas.Create');

@autoinject
export class Create {

  private pizza: IPizza;

  constructor(private router: Router,
              private pizzasService: PizzasService) {
    log.debug('constructor');
  }

  // ============ View Methods ============
  submit():void{
    log.debug('pizza', this.pizza);
    this.pizza.pirce = 3;
    this.pizzasService.post(this.pizza).then(
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
