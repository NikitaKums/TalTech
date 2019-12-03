import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ToppingsService} from "../services/toppings-service";
import {ITopping} from "../interfaces/ITopping";

export var log = LogManager.getLogger('toppings.Edit');

@autoinject
export class Edit {

  private topping: ITopping;

  constructor(private router: Router,
              private toppingsService: ToppingsService) {
    log.debug('constructor');
  }
  // ============ View methods ==============
  submit():void{
    log.debug('topping', this.topping);
    this.toppingsService.put(this.topping!).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("toppingsIndex");
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
    this.toppingsService.fetch(params.id).then(
      topping => {
        log.debug('topping', topping);
        this.topping = topping;
      });
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
