import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ISale} from "../interfaces/ISale";
import {SalesService} from "../services/sales-service";

export var log = LogManager.getLogger('Sales.Delete');

@autoinject
export class Delete {

  private sale: ISale;

  constructor(private router: Router,
              private salesService: SalesService) {
    log.debug('constructor');
  }
  
  // ============ View Methods ================
  submit():void{
    log.debug('submit', this.sale);

    if (this.sale.productsSoldCount == 0){
      this.salesService.delete(this.sale.id).then(
        response => {
          if (response.status == 200){
            this.router.navigateToRoute("salesIndex");
          } else {
            log.debug("response", response.status);
          }
        }
      );
    } else {
      alert("Stop hacking!");
    }
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
    this.salesService.fetch(params.id).then(
      sale => {
        log.debug("sale", sale);
        this.sale = sale;
        this.sale.dateString = this.sale.saleInitialCreationTime.toString().replace("T", " ");
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
