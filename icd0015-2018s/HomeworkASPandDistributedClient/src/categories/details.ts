import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ICategory} from "../interfaces/ICategory";
import {CategoriesService} from "../services/categories-service";
import {ProductsService} from "../services/products-service";
import {IIdAndNameProduct} from "../interfaces/IIdAndNameProduct";

export var log = LogManager.getLogger('Categories.Details');

@autoinject
export class Details {

  private category: ICategory  | null = null;
  private products: IIdAndNameProduct[] = [];

  constructor(private router: Router,
              private categoriesService: CategoriesService,
              private productsService: ProductsService) {
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
    log.debug('activate', params.id);
    this.categoriesService.fetch(params.id).then(
      category => {
        log.debug("category", category);
        this.category = category;
      }
    );
    
    this.productsService.fetchForProductsIdAndNameByCategoryId(params.id).then(
      products => {
        log.debug("products", products);
        this.products = products;
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
