import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IReturn} from "../interfaces/IReturn";
import {ReturnsService} from "../services/returns-service";

export var log = LogManager.getLogger('Returns.Edit');

@autoinject
export class Edit {

  private return: IReturn;

  constructor(private router: Router,
    private returnsService: ReturnsService) {
    log.debug('constructor');
  }

  // ============ View methods ==============
  submit():void{
    log.debug('return', this.return);

    if (this.return == undefined || this.return.description.trim().length == 0){
      alert("Return description cant be empty!");
      return;
    }
    
    this.returnsService.put(this.return!).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("returnsIndex");
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
    this.returnsService.fetch(params.id).then(
      jsonData => {
        log.debug('return', jsonData);
        this.return = jsonData
      });
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
