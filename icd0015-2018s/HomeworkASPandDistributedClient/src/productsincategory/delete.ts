import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IProductInCategory} from "../interfaces/IProductInCategory";
import {ProductsInCategoryService} from "../services/products-in-category-service";

export var log = LogManager.getLogger('ProductsInCategory.Delete');

@autoinject
export class Delete {

  private productInCategory: IProductInCategory;

  constructor(private router: Router,
              private productsInCategory: ProductsInCategoryService) {
    log.debug('constructor');
  }

  // ============ View Methods ================
  submit(): void {
    log.debug('submit', this.productInCategory);

    this.productsInCategory.delete(this.productInCategory.id).then(
      response => {
        if (response.status == 200) {
          this.router.navigateToRoute("categoriesDetails", {id: this.productInCategory.categoryId});
        } else {
          log.debug("response", response.status);
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
    this.productsInCategory.fetch(params.id)
      .then(
        jsonData => {
          log.debug('jsonData', jsonData);
          this.productInCategory = jsonData;
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
