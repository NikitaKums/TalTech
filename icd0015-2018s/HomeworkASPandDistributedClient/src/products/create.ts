import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ProductsService} from "../services/products-service";
import {IProduct} from "../interfaces/IProduct";
import {IManuFacturer} from "../interfaces/IManuFacturer";
import {ManufacturersService} from "../services/manufacturers-service";
import {ShopsService} from "../services/shops-service";
import {IShop} from "../interfaces/IShop";
import {CalcSellPrice, sellPriceValue} from "../js/site"

export var log = LogManager.getLogger('Products.Create');

@autoinject
export class Create {
  
  private product: IProduct;
  private manuFacturers: IManuFacturer[] = [];
  private shop: IShop;
  
  constructor(private router: Router,
              private productsService: ProductsService,
              private manuFacturerService: ManufacturersService,
              private shopsService: ShopsService) {
    log.debug('constructor');
  }

  // ============ View Methods ============
  submit():void{
    log.debug('product', this.product);
    this.product.sellPrice = sellPriceValue;
    this.product.inventoryId = this.shop.inventoryId;
    this.product.shopId = this.shop.id;
    this.productsService.post(this.product).then(
      response => {
        if (response.status == 201){
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
    this.manuFacturerService.fetchAll(undefined, undefined, undefined).then(
      jsonData => {
        log.debug("manuFacturer", jsonData);
        this.manuFacturers = jsonData;
      }
    );

    this.shopsService.fetchForShop().then(
      jsonData => {
        log.debug("shop", jsonData);
        this.shop = jsonData;
      }
    );
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
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
