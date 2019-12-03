import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ITopping} from "../interfaces/ITopping";
import {ToppingsService} from "../services/toppings-service";

export var log = LogManager.getLogger('toppings.Create');

@autoinject
export class Create {

  private topping: ITopping;

  constructor(private router: Router,
              private toppingsService: ToppingsService) {
    log.debug('constructor');
  }

  // ============ View Methods ============
  submit():void{
    log.debug('topping', this.topping);

    this.toppingsService.post(this.topping).then(
      response => {
        if (response.status == 201){
          this.router.navigateToRoute("toppingsIndex");
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
