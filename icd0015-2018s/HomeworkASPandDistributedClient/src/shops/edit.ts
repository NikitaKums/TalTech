import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IShop} from "../interfaces/IShop";
import {ShopsService} from "../services/shops-service";

export var log = LogManager.getLogger('Shops.Edit');

@autoinject
export class Edit {

  private shop: IShop | null = null;

  constructor(private router: Router,
              private shopsService: ShopsService) {
    log.debug('constructor');
  }
  // ============ View methods ==============
 /* submit():void{
    log.debug('shop', this.shop);
    if (this.shop == undefined || this.shop.shopName.trim().length == 0){
      alert("Shop name cant be empty!");
      return;
    }
    this.shopsService.put(this.shop!).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("shopsIndex");
        } else {
          alert("Invalid data entered!");
          log.error('Error in response!', response);
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
