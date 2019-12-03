import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IProductSold} from "../interfaces/IProductSold";
import {ProductsSoldService} from "../services/products-sold-service";

export var log = LogManager.getLogger('ProductsSold.Delete');

@autoinject
export class Delete {

  private productSold: IProductSold;

  constructor(private router: Router,
              private productsSoldService: ProductsSoldService) {
    log.debug('constructor');
  }

  // ============ View Methods ================
  submit(): void {
    log.debug('submit', this.productSold);

    this.productsSoldService.delete(this.productSold.id).then(
      response => {
        if (response.status == 200) {
          this.router.navigateToRoute("salesDetails", {id: this.productSold.saleId});
        } else {
          log.debug("response", response.status);
        }
      }
    );
  }
  
  submitRestore(): void{
    this.productsSoldService.deleteWithRestore(this.productSold.id).then(
      response => {
        if (response.status == 200) {
          this.router.navigateToRoute("salesDetails", {id: this.productSold.saleId});
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
    this.productsSoldService.fetch(params.id)
      .then(
        jsonData => {
          log.debug('jsonData', jsonData);
          this.productSold = jsonData;
          this.productSold.dateString = this.productSold.productSoldTime.toString().replace("T", " ");
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
