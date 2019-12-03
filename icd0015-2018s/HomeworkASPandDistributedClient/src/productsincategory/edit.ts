import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IIdAndNameProduct} from "../interfaces/IIdAndNameProduct";
import {ProductsService} from "../services/products-service";
import {ICategoryIdAndName} from "../interfaces/ICategoryIdAndName";
import {IProductInCategory} from "../interfaces/IProductInCategory";
import {CategoriesService} from "../services/categories-service";
import {ProductsInCategoryService} from "../services/products-in-category-service";

export var log = LogManager.getLogger('ProductsInCategory.Edit');

@autoinject
export class Edit {

  private productInCategory: IProductInCategory;
  private categories: ICategoryIdAndName[] = [];
  private products: IIdAndNameProduct[] = [];
  private returnCategoryId: number;

  constructor(private router: Router,
              private categoriesService: CategoriesService,
              private productsService: ProductsService,
              private productsInCategoryService: ProductsInCategoryService) {
    log.debug('constructor');
  }

  // ============ View Methods ============
  submit():void{
    log.debug('productInCategory', this.productInCategory);

    this.productsInCategoryService.put(this.productInCategory).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("categoriesDetails", {id: this.returnCategoryId});
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
    this.productsInCategoryService.fetch(params.id).then(
      productInCategory => {
        log.debug('productInCategory', productInCategory);
        this.productInCategory = productInCategory;
        this.returnCategoryId = this.productInCategory.categoryId;
      }
    );
    
    this.productsService.fetchForProductsIdAndName().then(
      jsonData => {
        log.debug("products", jsonData);
        this.products = jsonData;
      }
    );

    this.categoriesService.fetchAll(undefined, undefined, undefined).then(
      jsonData => {
        log.debug("categories", jsonData);
        this.categories = jsonData;
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
