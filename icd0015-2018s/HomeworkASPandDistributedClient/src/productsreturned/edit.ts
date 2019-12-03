import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IProductReturned} from "../interfaces/IProductReturned";
import {ProductsReturnedService} from "../services/products-returned-service";
import {IIdAndNameProduct} from "../interfaces/IIdAndNameProduct";
import {IIdAndDescReturn} from "../interfaces/IIdAndDescReturn";
import {ProductsService} from "../services/products-service";
import {ReturnsService} from "../services/returns-service";

export var log = LogManager.getLogger('ProductsReturned.Index');

@autoinject
export class Index {

  private productReturned: IProductReturned;
  private products: IIdAndNameProduct[] = [];
  private returns: IIdAndDescReturn[] = []; 
  private returnReturnId: number;

  constructor(private router: Router,
              private productsService: ProductsService,
              private returnsService: ReturnsService,
              private productsReturnedService: ProductsReturnedService) {
    log.debug('constructor');
  }
  
  // ============ View Methods ============
  submit():void{
    log.debug('productReturned', this.productReturned);
    try {
      this.productReturned.productReturnedTime.setMinutes(this.productReturned.productReturnedTime.getMinutes() + 180);
    } catch (e) {
    }
    this.productsReturnedService.put(this.productReturned).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("returnsDetails", {id: this.returnReturnId});
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
    this.productsReturnedService.fetch(params.id).then(
      productReturned => {
        log.debug('productReturned', productReturned);
        this.productReturned = productReturned;
        this.returnReturnId = this.productReturned.returnId;
      }
    );

    this.productsService.fetchForProductsIdAndName().then(
      jsonData => {
        log.debug("products", jsonData);
        this.products = jsonData;
      }
    );

    this.returnsService.fetchForReturnIdAndName().then(
      jsonData => {
        log.debug("returns", jsonData);
        this.returns = jsonData;
      }
    )
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
