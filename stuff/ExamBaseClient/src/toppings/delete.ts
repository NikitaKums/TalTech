import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ITopping} from "../interfaces/ITopping";
import {ToppingsService} from "../services/toppings-service";

export var log = LogManager.getLogger('toppings.Delete');

@autoinject
export class Delete {

  private topping: ITopping;

  constructor(private router: Router,
              private toppingsService: ToppingsService) {
    log.debug('constructor');
  }

  // ============ View Methods ================
  submit():void{
    log.debug('submit', this.topping);

    this.toppingsService.delete(this.topping.id).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("toppingsIndex");
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
    this.toppingsService.fetch(params.id).then(
      topping => {
        log.debug("topping", topping);
        this.topping = topping;
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
