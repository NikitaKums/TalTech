import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ProductsService} from "../services/products-service";
import {IProduct} from "../interfaces/IProduct";
import {IManuFacturer} from "../interfaces/IManuFacturer";
import {ManufacturersService} from "../services/manufacturers-service";
import {CalcSellPrice, sellPriceValue} from "../js/site"

export var log = LogManager.getLogger('Products.Edit');

@autoinject
export class Edit {

  private product: IProduct;
  private manuFacturers: IManuFacturer[] = [];

  constructor(private router: Router,
              private productsService: ProductsService,
              private manuFacturerService: ManufacturersService) {
    
    log.debug('constructor');
  }

  // ============ View Methods ============
  submit():void{
    log.debug('product', this.product);
    this.product.sellPrice = sellPriceValue;
    this.productsService.put(this.product).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("productsIndex");
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
    CalcSellPrice();
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
        log.debug('product', product);
        this.product = product;
      });

    this.manuFacturerService.fetchAll(undefined, undefined, undefined).then(
      manuFacturers => {
        log.debug('manuFacturers', manuFacturers);
        this.manuFacturers = manuFacturers;
      });
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
