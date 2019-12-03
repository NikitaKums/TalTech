import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ICategory} from "../interfaces/ICategory";
import {CategoriesService} from "../services/categories-service";
import {IShop} from "../interfaces/IShop";

export var log = LogManager.getLogger('Categories.Create');

@autoinject
export class Create {

  private category: ICategory;
  private shop: IShop;
  
  constructor(private router: Router,
    private categoriesService: CategoriesService) {
    log.debug('constructor');
  }
  
  // ============ View Methods ============
  submit():void{
    log.debug('category', this.category);
    if (this.category == undefined || this.category.categoryName.trim().length == 0){
      alert("Category name cannot be empty!");
      return;
    }
    this.category.shopId = this.shop.id;
    this.categoriesService.post(this.category).then(
      response => {
        if (response.status == 201){
          this.router.navigateToRoute("categoriesIndex");
        } else {
          alert("Invalid data entered");
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
    this.categoriesService.fetchForShop().then(
      jsonData => {
        log.debug("d", jsonData);
        this.shop = jsonData;
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
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
