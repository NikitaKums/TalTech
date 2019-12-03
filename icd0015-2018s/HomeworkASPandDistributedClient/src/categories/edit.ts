import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ICategory} from "../interfaces/ICategory";
import {IShop} from "../interfaces/IShop";
import {CategoriesService} from "../services/categories-service";

export var log = LogManager.getLogger('Categories.Edit');

@autoinject
export class Edit {
  
  private category : ICategory;
  private shop: IShop;

  constructor(private router: Router,
              private categoriesService: CategoriesService) {
    log.debug('constructor');
  }
  // ============ View methods ==============
  submit():void{
    log.debug('category', this.category, this.shop);
    if (this.category == undefined || this.category.categoryName.trim().length == 0){
      alert("Category name cannot be empty!");
      return;
    }
    this.category.shopId = this.shop.id;
    this.categoriesService.put(this.category!).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("categoriesIndex");
        } else {
          alert("Invalid data entered");
          log.error('Error in response!', response);
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
    this.categoriesService.fetch(params.id).then(
      category => {
        log.debug('category', category);
        this.category = category;
      });
    
    this.categoriesService.fetchForShop().then(
      shop => {
        this.shop = shop
      });
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
