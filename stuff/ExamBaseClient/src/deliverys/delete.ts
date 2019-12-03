import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {DeliverysService} from "../services/deliverys-service";
import {IDelivery} from "../interfaces/IDelivery";

export var log = LogManager.getLogger('Comments.Delete');

@autoinject
export class Delete {

  private delivery: IDelivery;

  constructor(private router: Router,
              private deliverysService: DeliverysService) {
    log.debug('constructor');
  }

  // ============ View Methods ================
  submit():void{
    log.debug('delivery', this.delivery);

    this.deliverysService.delete(this.delivery.id).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("deliverysIndex");
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
    this.deliverysService.fetch(params.id).then(
      delivery => {
        log.debug("delivery", delivery);
        this.delivery = delivery;
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
