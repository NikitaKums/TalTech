import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ISale} from "../interfaces/ISale";
import {SalesService} from "../services/sales-service";
import {IProductSold} from "../interfaces/IProductSold";
import {ProductsSoldService} from "../services/products-sold-service";

export var log = LogManager.getLogger('Sales.Details');

@autoinject
export class Details {

  private sale: ISale;
  private productsSold: IProductSold[] = [];

  constructor(private router: Router,
              private salesService: SalesService,
              private productsSoldService: ProductsSoldService) {
    log.debug('constructor');
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
    this.productsSoldService.fetchForInfoBySaleId(params.id).then(
      productsSold => {
        log.debug("productInOrder", productsSold);
        this.productsSold = productsSold;
        for (let productSold of this.productsSold){
          productSold.dateString = productSold.productSoldTime.toString().replace("T", " ");
        }
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
