import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {CategoriesService} from "../services/categories-service";
import {ICategory} from "../interfaces/ICategory";

export var log = LogManager.getLogger('Categories.Delete');

@autoinject
export class Delete {
  
  private category: ICategory;
  
  constructor(private router: Router,
              private categoriesService: CategoriesService) {
    log.debug('constructor');
  }
  
  // ============ View Methods ================
  submit():void{
    
    if (this.category.categoryProductCount == 0){
      this.categoriesService.delete(this.category.id).then(
        response => {
          if (response.status == 200){
            this.router.navigateToRoute("categoriesIndex");
          } else {
            log.debug("response", response.status);
          }
        }
      );
    } else {
      alert("Stop hacking!");
    }
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
        log.debug("category", category);
        this.category = category;
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
