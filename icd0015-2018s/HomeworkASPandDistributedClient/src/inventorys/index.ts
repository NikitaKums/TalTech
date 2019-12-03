import {LogManager, View, autoinject, bindable} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IInventory} from "../interfaces/IInventory";
import {InventorysService} from "../services/inventorys-service";
import {IShop} from "../interfaces/IShop";

export var log = LogManager.getLogger('Inventorys.Index');

@autoinject
export class Index {

  private inventorys: IInventory[] = [];
  private shop: IShop;
  private entryCount: number;
  private pageSize: number = 10;
  private pageIndex: number = 1;
  private totalPages: number;
  
  @bindable private search: string = '';

  constructor(private router: Router,
    private inventorysService: InventorysService) {
    log.debug('constructor');
  }

  previousButtonClicked() {
    log.debug('previousButtonClicked');
    this.pageIndex = this.pageIndex - 1;
    this.loadData();
  }

  nextButtonClicked() {
    log.debug('nextButtonClicked');
    this.pageIndex = this.pageIndex + 1;
    this.loadData();
  }

  searchClicked() {
    log.debug('searchClicked', this.search);
    this.loadData();
  }

  searchResetClicked() {
    log.debug('searchResetClicked');
    this.search = '';
    this.loadData();
  }

  loadData(){
    this.inventorysService.fetchAll('?search=' + this.search, this.pageIndex, this.pageSize).then(
      jsonData => {
        this.inventorys = jsonData;
        for (let inventory of this.inventorys){
          inventory.dateString = inventory.inventoryCreationTime.toString().replace("T", " ");
        }
      }
    );

    this.inventorysService.getAmountOfEntries('?search=' + this.search).then(
      jsonData => {
        log.debug("entryAmount", jsonData);
        this.entryCount = jsonData;
        this.totalPages = Math.ceil(this.entryCount / this.pageSize);
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
    this.loadData();
    this.inventorysService.fetchForShop().then(
      jsonData => {
        log.debug('jsonData', jsonData);
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
