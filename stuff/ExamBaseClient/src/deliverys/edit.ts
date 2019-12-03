import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {DeliverysService} from "../services/deliverys-service";
import {IDelivery} from "../interfaces/IDelivery";

export var log = LogManager.getLogger('Delivery.Edit');

@autoinject
export class Edit {

  private delivery: IDelivery;

  constructor(private router: Router,
              private deliverysService: DeliverysService) {
    log.debug('constructor');
  }
  // ============ View methods ==============
  submit():void{
    log.debug('comment', this.delivery);
    this.deliverysService.put(this.delivery!).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("deliverysIndex");
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
    this.deliverysService.fetch(params.id).then(
      delivery => {
        log.debug('delivery', delivery);
        this.delivery = delivery;
      });
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
