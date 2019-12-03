import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ProductsService} from "../services/products-service";
import {IProduct} from "../interfaces/IProduct";

export var log = LogManager.getLogger('Products.Delete');

@autoinject
export class Delete {
  
  private product: IProduct;
  
  constructor(private router: Router,
    private productsService: ProductsService) {
    log.debug('constructor');
  }

  // ============ View Methods ================
  submit():void{
    log.debug('submit', this.product);

    if (this.product.productReturnsCount == 0 && this.product.productsInOrdersCount == 0 && this.product.productsSoldCount == 0 
      && this.product.productsWithDefectCount == 0 && this.product.commentDTOs.length == 0 && this.product.categoryDTOs.length == 0){
      this.productsService.delete(this.product.id).then(
        response => {
          if (response.status == 200){
            this.router.navigateToRoute("productsIndex");
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
    this.productsService.fetch(params.id).then(
      product => {
        log.debug("product", product);
        this.product = product;
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
