import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IProductReturned} from "../interfaces/IProductReturned";
import {ProductsReturnedService} from "../services/products-returned-service";
import {IIdAndNameProduct} from "../interfaces/IIdAndNameProduct";
import {ProductsService} from "../services/products-service";
import {IIdAndDescReturn} from "../interfaces/IIdAndDescReturn";
import {ReturnsService} from "../services/returns-service";

export var log = LogManager.getLogger('ProductsReturned.Create');

@autoinject
export class Create {

  private productReturned: IProductReturned;
  private products: IIdAndNameProduct[] = [];
  private returns: IIdAndDescReturn[] = [];
  private selectedField: number;

  constructor(private router: Router,
              private productsService: ProductsService,
              private returnsService: ReturnsService,
              private productsReturnedService: ProductsReturnedService) {
    log.debug('constructor');
  }

  // ============ View Methods ============
  submit():void{
    log.debug('productReturned', this.productReturned);
    this.productReturned.productReturnedTime.setMinutes(this.productReturned.productReturnedTime.getMinutes() + 180);
    this.productReturned.id = 0;
    this.productReturned.returnId = this.selectedField;
    this.productsReturnedService.post(this.productReturned).then(
      response => {
        if (response.status == 201){
          this.router.navigateToRoute("returnsIndex");
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
    );
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
    this.selectedField = params.id;
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
