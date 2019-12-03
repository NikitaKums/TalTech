import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IToppingOnPizza} from "../interfaces/IToppingOnPizza";
import {ToppingsOnPizzaService} from "../services/toppings-on-pizza-service";

export var log = LogManager.getLogger('toppingonpizzas.Details');

@autoinject
export class Details {

  private toppingOnPizza: IToppingOnPizza;

  constructor(private router: Router,
              private toppingsOnPizzaService: ToppingsOnPizzaService) {
    log.debug('constructor');
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
    log.debug('activate', params.id);
    this.toppingsOnPizzaService.fetch(params.id).then(
      toppingOnPizza => {
        log.debug("toppingOnPizza", toppingOnPizza);
        this.toppingOnPizza = toppingOnPizza;
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
