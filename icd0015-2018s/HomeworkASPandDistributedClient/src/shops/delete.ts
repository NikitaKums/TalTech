import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IShop} from "../interfaces/IShop";
import {ShopsService} from "../services/shops-service";

export var log = LogManager.getLogger('Shops.Delete');

@autoinject
export class Delete {

  private shop: IShop;

  constructor(private router: Router,
              private shopsService: ShopsService) {
    log.debug('constructor');
  }

  // ============ View Methods ================
 /* submit():void{
    this.shopsService.delete(this.shop.id).then(
      response => {
        if (response.status == 200){
          this.router.navigateToRoute("shopsIndex");
        } else {
          log.debug("response", response.status);
          alert("Cannot delete a shop that has items associated with it.");     
        }
      }
    );
  }*/

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
    this.shopsService.fetch(params.id).then(
      shop => {
        log.debug("shop", shop);
        this.shop = shop;
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
