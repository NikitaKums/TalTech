import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IReturn} from "../interfaces/IReturn";
import {ReturnsService} from "../services/returns-service";
import {IShop} from "../interfaces/IShop";

export var log = LogManager.getLogger('Returns.Create');

@autoinject
export class Create {

  private return: IReturn;
  private shop: IShop;

  constructor(private router: Router,
    private returnsService: ReturnsService) {
    log.debug('constructor');
  }

  // ============ View Methods ============
  submit():void{
    log.debug('return', this.return);
    if (this.return == undefined || this.return.description.trim().length == 0){
      alert("Return description cant be empty!");
      return;
    }
    this.return.shopId = this.shop.id;
    this.returnsService.post(this.return).then(
      response => {
        if (response.status == 201){
          this.router.navigateToRoute("returnsIndex");
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
    this.returnsService.fetchForShop().then(
      jsonData => {
        log.debug('jsonData', jsonData);
        this.shop = jsonData;
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
