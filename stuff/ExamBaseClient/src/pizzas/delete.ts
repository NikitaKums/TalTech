import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IPizza} from "../interfaces/IPizza";
import {PizzasService} from "../services/pizzas-service";

export var log = LogManager.getLogger('Comments.Delete');

@autoinject
export class Delete {

  private pizza: IPizza;

  constructor(private router: Router,
              private pizzasService: PizzasService) {
    log.debug('constructor');
  }

  // ============ View Methods ================
  submit():void{
    log.debug('submit', this.pizza);

    this.pizzasService.delete(this.pizza.id).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("pizzasIndex");
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
    this.pizzasService.fetch(params.id).then(
      pizza => {
        log.debug("pizza", pizza);
        this.pizza = pizza;
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
