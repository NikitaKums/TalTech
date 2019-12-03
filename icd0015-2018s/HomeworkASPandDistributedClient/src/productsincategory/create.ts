import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ProductsService} from "../services/products-service";
import {IIdAndNameProduct} from "../interfaces/IIdAndNameProduct";
import {IProductInCategory} from "../interfaces/IProductInCategory";
import {ICategoryIdAndName} from "../interfaces/ICategoryIdAndName";
import {CategoriesService} from "../services/categories-service";
import {ProductsInCategoryService} from "../services/products-in-category-service";

export var log = LogManager.getLogger('ProductsInCategory.Create');

@autoinject
export class Create {
  
  private productInCategory: IProductInCategory;
  private categories: ICategoryIdAndName[] = [];
  private products: IIdAndNameProduct[] = [];
  private selectedField: number;
  
  constructor(private router: Router,
              private categoriesService: CategoriesService,
              private productsService: ProductsService,
              private productsInCategoryService: ProductsInCategoryService) {
    log.debug('constructor');
  }

  // ============ View Methods ============
  submit():void{
    log.debug('productInCategory', this.productInCategory, this.selectedField);
    this.productInCategory.id = 0;
    this.productInCategory.categoryId = this.selectedField;
    this.productsInCategoryService.post(this.productInCategory).then(
      response => {
        if (response.status == 201){
          this.router.navigateToRoute("categoriesIndex");
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

    this.categoriesService.fetchAll(undefined, undefined, undefined).then(
      jsonData => {
        log.debug("categories", jsonData);
        this.categories = jsonData;
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
