import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IDefect} from "../interfaces/IDefect";
import {DefectsService} from "../services/defects-service";

export var log = LogManager.getLogger('Defects.Edit');

@autoinject
export class Edit {
  
  private defect: IDefect;

  constructor(private defectsService: DefectsService,
              private router: Router) {
    log.debug('constructor');
  }

  // ============ View methods ==============
  submit():void{
    log.debug('defect', this.defect);
    if (this.defect == undefined || this.defect.description.trim().length == 0){
      alert("Defect description cant be empty!");
      return;
    }
    this.defectsService.put(this.defect!).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("defectsIndex");
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
    this.defectsService.fetch(params.id).then(
      defect => {
        log.debug('defect', defect);
        this.defect = defect;
      });
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
