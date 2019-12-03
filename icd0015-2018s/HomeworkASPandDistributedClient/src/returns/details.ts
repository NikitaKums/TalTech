import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IReturn} from "../interfaces/IReturn";
import {ReturnsService} from "../services/returns-service";
import {ProductsReturnedService} from "../services/products-returned-service";
import {IProductReturned} from "../interfaces/IProductReturned";

export var log = LogManager.getLogger('Returns.Details');

@autoinject
export class Details {

  private return: IReturn | null = null;
  private productsReturned: IProductReturned[] = [];

  constructor(private router: Router,
              private returnsService: ReturnsService,
              private productsReturnedService: ProductsReturnedService) {
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
    this.returnsService.fetch(params.id).then(
      jsonData => {
        log.debug("return", jsonData);
        this.return = jsonData;
      }
    );

    this.productsReturnedService.fetchForInfoByReturnId(params.id).then(
      jsonData => {
        log.debug("return", jsonData);
        this.productsReturned = jsonData;
        for (let productReturned of this.productsReturned){
          productReturned.dateString = productReturned.productReturnedTime.toString().replace("T", " ");
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
