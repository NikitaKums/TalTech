import {LogManager, View, autoinject, bindable} from "aurelia-framework";
import {RouteConfig, NavigationInstruction} from "aurelia-router";
import {DeliverysService} from "../services/deliverys-service";
import {IDelivery} from "../interfaces/IDelivery";

export var log = LogManager.getLogger('Delivery.Index');

@autoinject
export class Index {

  private deliverys: IDelivery[] = [];
  @bindable private search: string = '';

  constructor(private deliverysService: DeliverysService) {
    log.debug('constructor');
  }

  searchClicked() {
    log.debug('searchClicked', this.search);
    this.loadData();
  }

  searchResetClicked() {
    log.debug('searchResetClicked');
    this.search = '';
    this.loadData();
  }

  loadData(){
    this.deliverysService.fetchAll('?search=' + this.search).then(
      jsonData => {
        this.deliverys = jsonData;
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
    this.loadData();
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
