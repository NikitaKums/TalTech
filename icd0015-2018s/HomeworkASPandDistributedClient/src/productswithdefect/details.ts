import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ProductsWithDefectService} from "../services/products-with-defect-service";
import {IProductWithDefect} from "../interfaces/IProductWithDefect";

export var log = LogManager.getLogger('ProductsWithDefect.Details');

@autoinject
export class Details {
  
  private productWithDefect: IProductWithDefect;
  
  constructor(private router: Router,
              private productsWithDefectService: ProductsWithDefectService) {
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
    this.productsWithDefectService.fetch(params.id).then(
      jsonData => {
        log.debug('jsonData', jsonData);
        this.productWithDefect = jsonData;
        this.productWithDefect.dateString = this.productWithDefect.defectRecordingTime.toString().replace("T", " ");
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
