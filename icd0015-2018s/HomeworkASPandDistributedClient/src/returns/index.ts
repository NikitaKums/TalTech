import {LogManager, View, autoinject, bindable} from "aurelia-framework";
import {RouteConfig, NavigationInstruction} from "aurelia-router";
import {IReturn} from "../interfaces/IReturn";
import {ReturnsService} from "../services/returns-service";

export var log = LogManager.getLogger('Returns.Index');

@autoinject
export class Index {

  private returns: IReturn[] = [];
  private entryCount: number;
  private pageSize: number = 10;
  private pageIndex: number = 1;
  private totalPages: number;
  
  @bindable private search: string = '';

  constructor(
    private returnsService: ReturnsService) {
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
    this.returnsService.fetchAll('?search=' + this.search, this.pageIndex, this.pageSize).then(
      jsonData => {
        this.returns = jsonData;
      }
    );

    this.returnsService.getAmountOfEntries('?search=' + this.search).then(
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
