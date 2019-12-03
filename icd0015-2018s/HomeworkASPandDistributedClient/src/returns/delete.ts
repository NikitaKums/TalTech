import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IReturn} from "../interfaces/IReturn";
import {ReturnsService} from "../services/returns-service";

export var log = LogManager.getLogger('Returns.Delete');

@autoinject
export class Delete {

  private return: IReturn;

  constructor(private router: Router,
    private returnsService: ReturnsService) {
    log.debug('constructor');
  }

  // ============ View Methods ================
  submit():void{
    log.debug('return', this.return);

    if (this.return.productsReturnedCount == 0){
      this.returnsService.delete(this.return.id).then(
        response => {
          if (response.status == 200){
            this.router.navigateToRoute("returnsIndex");
          } else {
            log.debug("response", response.status);
            alert("Cannot delete a return that has items associated with it.");
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
    this.returnsService.fetch(params.id).then(
      jsonData => {
        log.debug("return", jsonData);
        this.return = jsonData;
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
