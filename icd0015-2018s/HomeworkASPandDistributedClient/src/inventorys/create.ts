import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IInventory} from "../interfaces/IInventory";
import {InventorysService} from "../services/inventorys-service";
import {IShop} from "../interfaces/IShop";

export var log = LogManager.getLogger('Inventorys.Create');

@autoinject
export class Create {

  private inventory: IInventory;
  private shop: IShop;

  constructor(private router: Router,
    private inventorysService: InventorysService) {
    log.debug('constructor');
  }


  // ============ View Methods ============
  submit():void{
    log.debug('inventory', this.inventory);
    this.inventory.inventoryCreationTime.setMinutes(this.inventory.inventoryCreationTime.getMinutes() + 120);
    this.inventory.shopId = this.shop.id;
    this.inventorysService.post(this.inventory).then(
      response => {
        if (response.status == 201){
          this.router.navigateToRoute("inventorysIndex");
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
    this.inventorysService.fetchForShop().then(
      jsonData => {
        log.debug("shop", jsonData);
        this.shop = jsonData;
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
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
