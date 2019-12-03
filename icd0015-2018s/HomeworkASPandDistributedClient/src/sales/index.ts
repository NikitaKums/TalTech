import {LogManager, View, autoinject, bindable} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ISale} from "../interfaces/ISale";
import {SalesService} from "../services/sales-service";
import {IAppUser} from "../interfaces/IAppUser";
import {AppUserService} from "../services/app-user-service";

export var log = LogManager.getLogger('Sales.Index');

@autoinject
export class Index {

  private sales: ISale[] = [];
  private user: IAppUser;
  private entryCount: number;
  private pageSize: number = 10;
  private pageIndex: number = 1;
  private totalPages: number;
  private overAllTotalSaleAmount: number | undefined;
  private todayOverAllTotalSaleAmount: number | undefined;
  
  @bindable private search: string = '';

  constructor(private router: Router,
              private appUserService: AppUserService,
              private salesService: SalesService) {
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
    this.salesService.fetchAll('?search=' + this.search, this.pageIndex, this.pageSize).then(
      jsonData => {
        this.sales = jsonData;
        for (let sale of this.sales){
          sale.dateString = sale.saleInitialCreationTime.toString().replace("T", " ");
        }
      }
    );

    this.salesService.getAmountOfEntries('?search=' + this.search).then(
      jsonData => {
        log.debug("entryAmount", jsonData);
        this.entryCount = jsonData;
        this.totalPages = Math.ceil(this.entryCount / this.pageSize);
      }
    );

    this.salesService.getTotalSaleAmounts().then(
      jsonData => {
        log.debug("sale amounts", jsonData);
        this.todayOverAllTotalSaleAmount = jsonData["overAllTodayTotalSaleAmount"];
        this.overAllTotalSaleAmount = jsonData["overAllTotalSaleAmount"];
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
    this.appUserService.getUserShopId().then(
      jsonData => {
        log.debug('jsonData', jsonData);
        this.user = jsonData;
        log.debug("user", this.user)
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
