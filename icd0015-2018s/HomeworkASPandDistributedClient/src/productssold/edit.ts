import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ProductsService} from "../services/products-service";
import {IIdAndNameProduct} from "../interfaces/IIdAndNameProduct";
import {IProductSold} from "../interfaces/IProductSold";
import {SalesService} from "../services/sales-service";
import {ProductsSoldService} from "../services/products-sold-service";
import {IIdAndDescSale} from "../interfaces/IIdAndDescSale";

export var log = LogManager.getLogger('ProductsSold.Edit');

@autoinject
export class Edit {

  private productSold: IProductSold;
  private sales: IIdAndDescSale[] = [];
  private products: IIdAndNameProduct[] = [];
  private saleSaleId: number;

  constructor(private router: Router,
              private salesService: SalesService,
              private productsService: ProductsService,
              private productsSoldService: ProductsSoldService) {
    log.debug('constructor');
  }

  // ============ View Methods ============
  submit():void{
    log.debug('productSold', this.productSold);
    try {
      this.productSold.productSoldTime.setMinutes(this.productSold.productSoldTime.getMinutes() + 180);
    } catch (e) {
    }
    this.productsSoldService.put(this.productSold).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("salesDetails", {id: this.saleSaleId});
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

    this.productsSoldService.fetch(params.id).then(
      productSold => {
        log.debug('productSold', productSold);
        this.productSold = productSold;
        this.saleSaleId = this.productSold.saleId;
      }
    );
    
    this.productsService.fetchForProductsIdAndName().then(
      jsonData => {
        log.debug("products", jsonData);
        this.products = jsonData;
      }
    );

    this.salesService.fetchForSaleIdAndName().then(
      jsonData => {
        log.debug("sales", jsonData);
        this.sales = jsonData;
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
