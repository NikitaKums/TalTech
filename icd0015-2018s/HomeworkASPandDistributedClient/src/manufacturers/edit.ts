import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ManufacturersService} from "../services/manufacturers-service";
import {IManuFacturer} from "../interfaces/IManuFacturer";

export var log = LogManager.getLogger('ManuFacturers.Edit');

@autoinject
export class Edit {
  
  private manuFacturer: IManuFacturer | null = null;
  
  constructor(private router: Router,
    private manuFacturersService: ManufacturersService) {
    log.debug('constructor');
  }

  // ============ View methods ==============
  submit():void{
    log.debug('manuFacturer', this.manuFacturer);
    
    if (this.manuFacturer == undefined || this.manuFacturer.manuFacturerName.trim().length == 0){
      alert("Manufacturer's name cant be empty!");
      return;
    }
    this.manuFacturersService.put(this.manuFacturer!).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("manufacturersIndex");
        } else {
          alert("Invalid data entered!");
          log.error('Error in response!', response);
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
    this.manuFacturersService.fetch(params.id).then(
      manuFacturer => {
        this.manuFacturer = manuFacturer
      });
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
