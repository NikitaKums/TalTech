import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IInventory} from "../interfaces/IInventory";
import {InventorysService} from "../services/inventorys-service";

export var log = LogManager.getLogger('Inventorys.Edit');

@autoinject
export class Edit {

  private inventory: IInventory;

  constructor(private router: Router,
    private inventorysService: InventorysService) {
    log.debug('constructor');
  }

  // ============ View Methods ============
  submit():void{
    log.debug('inventory', this.inventory);
    try {
      this.inventory.inventoryCreationTime.setMinutes(this.inventory.inventoryCreationTime.getMinutes() + 120);
    } catch (e) {
    }
    this.inventorysService.put(this.inventory).then(
      response => {
        if (response.status == 204){
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
        log.debug('inventory', inventory);
        this.inventory = inventory;
      });
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
