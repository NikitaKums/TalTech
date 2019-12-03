import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ProductsService} from "../services/products-service";
import {IIdAndNameProduct} from "../interfaces/IIdAndNameProduct";
import {IProductSold} from "../interfaces/IProductSold";
import {SalesService} from "../services/sales-service";
import {ProductsSoldService} from "../services/products-sold-service";
import {IIdAndDescSale} from "../interfaces/IIdAndDescSale";

export var log = LogManager.getLogger('ProductsSold.Create');

@autoinject
export class Create {

  private productSold: IProductSold;
  private sales: IIdAndDescSale[] = [];
  private products: IIdAndNameProduct[] = [];  
  private selectedField: number;

  constructor(private router: Router,
              private salesService: SalesService,
              private productsService: ProductsService,
              private productsSoldService: ProductsSoldService) {
    log.debug('constructor');
  }

  // ============ View Methods ============
  submit():void{
    log.debug('productSold', this.productSold);
    this.productSold.productSoldTime.setMinutes(this.productSold.productSoldTime.getMinutes() + 180);
    this.productSold.id = 0;
    this.productSold.saleId = this.selectedField;
    this.productsSoldService.post(this.productSold).then(
      response => {
        if (response.status == 201){
          this.router.navigateToRoute("salesIndex");
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

    this.salesService.fetchForSaleIdAndName().then(
      jsonData => {
        log.debug("sales", jsonData);
        this.sales = jsonData;
      }
    )
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
