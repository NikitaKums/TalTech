import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IInventory} from "../interfaces/IInventory";
import {InventorysService} from "../services/inventorys-service";

export var log = LogManager.getLogger('Inventorys.Delete');

@autoinject
export class Delete {

  private inventory: IInventory;

  constructor(private router: Router,
    private inventorysService: InventorysService) {
    log.debug('constructor');
  }

  // ============ View Methods ================
  submit():void{
    log.debug('submit', this.inventory);

    if (this.inventory.productCount == 0){
      this.inventorysService.delete(this.inventory.id).then(
        response => {
          if (response.status == 200){
            this.router.navigateToRoute("inventorysIndex");
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
    this.inventorysService.fetch(params.id).then(
      inventory => {
        log.debug("inventory", inventory);
        this.inventory = inventory;
        this.inventory.dateString = this.inventory.inventoryCreationTime.toString().replace("T", " ");
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
