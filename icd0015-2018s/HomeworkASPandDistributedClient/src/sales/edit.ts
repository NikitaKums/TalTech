import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ISale} from "../interfaces/ISale";
import {SalesService} from "../services/sales-service";

export var log = LogManager.getLogger('Sales.Edit');

@autoinject
export class Edit {

  private sale: ISale;

  constructor(private router: Router,
              private salesService: SalesService) {
    log.debug('constructor');
  }
  
  // ============ View Methods ============
  submit():void{
    log.debug('sale', this.sale);
    try {
      this.sale.saleInitialCreationTime.setMinutes(this.sale.saleInitialCreationTime.getMinutes() + 180);
    } catch (e) {
    }
    this.salesService.put(this.sale).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("salesIndex");
        } else {
          alert("Invalid data entered!");
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
    this.salesService.fetch(params.id).then(
      sale => {
        log.debug('sale', sale);
        this.sale = sale;
      });

  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
