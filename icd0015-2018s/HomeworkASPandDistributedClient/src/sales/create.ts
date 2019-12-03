import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ISale} from "../interfaces/ISale";
import {SalesService} from "../services/sales-service";
import {IAppUser} from "../interfaces/IAppUser";
import {AppUserService} from "../services/app-user-service";

export var log = LogManager.getLogger('Sales.Create');

@autoinject
export class Create {

  private sale: ISale;
  private appUser: IAppUser;

  constructor(private router: Router,
              private salesService: SalesService,
              private appUserService: AppUserService) {
    log.debug('constructor');
  }
  
  // ============ View Methods ============
  submit():void{
    log.debug('order', this.sale, this.appUser.id);
    this.sale.saleInitialCreationTime.setMinutes(this.sale.saleInitialCreationTime.getMinutes() + 180);
    this.sale.appUserId = this.appUser.id;
    this.salesService.post(this.sale).then(
      response => {
        if (response.status == 201){
          this.router.navigateToRoute("salesIndex");
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
    this.appUserService.fetchForCurrentUser().then(
      jsonData => {
        log.debug("appUser", jsonData);
        this.appUser = jsonData;
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
