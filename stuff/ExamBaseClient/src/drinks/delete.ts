import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {DrinksService} from "../services/drinks-service";
import {IDrink} from "../interfaces/IDrink";

export var log = LogManager.getLogger('Drinks.Delete');

@autoinject
export class Delete {

  private drink: IDrink;

  constructor(private router: Router,
              private drinksService: DrinksService) {
    log.debug('constructor');
  }

  // ============ View Methods ================
  submit():void{
    log.debug('submit', this.drink);

    this.drinksService.delete(this.drink.id).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("drinksIndex");
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
    this.drinksService.fetch(params.id).then(
      drink => {
        log.debug("drink", drink);
        this.drink = drink;
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
