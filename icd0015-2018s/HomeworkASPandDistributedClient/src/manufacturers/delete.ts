import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ManufacturersService} from "../services/manufacturers-service";
import {IManuFacturer} from "../interfaces/IManuFacturer";

export var log = LogManager.getLogger('ManuFacturers.Delete');

@autoinject
export class Delete {
  
  private manuFacturer: IManuFacturer;
  
  constructor(private router: Router,
    private manuFacturersService: ManufacturersService) {
    log.debug('constructor');
  }


  // ============ View Methods ================
  submit():void{
    log.debug('submit', this.manuFacturer);

    if (this.manuFacturer.productCount == 0){
      this.manuFacturersService.delete(this.manuFacturer.id).then(
        response => {
          if (response.status == 200){
            this.router.navigateToRoute("manufacturersIndex");
          } else {
            log.debug("response", response.status);
            alert("Cannot delete a manufacturer that has items associated with it.");
          }
        }
      );
    } else {
      alert("Stop hacking!")
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
    this.manuFacturersService.fetch(params.id).then(
      manuFacturer => {
        log.debug("manuFacturer", manuFacturer);
        this.manuFacturer = manuFacturer;
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
