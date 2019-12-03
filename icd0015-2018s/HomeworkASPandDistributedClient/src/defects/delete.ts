import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IDefect} from "../interfaces/IDefect";
import {DefectsService} from "../services/defects-service";

export var log = LogManager.getLogger('Defects.Delete');

@autoinject
export class Delete {
  
  private defect: IDefect;
  
  constructor(private router: Router,
              private defectsService: DefectsService) {
    log.debug('constructor');
  }

  // ============ View Methods ================
  submit():void{
    log.debug('submit', this.defect);
    
    if (this.defect.productsWithDefectCount == 0){
      this.defectsService.delete(this.defect.id).then(
        response => {
          if (response.status == 200){
            this.router.navigateToRoute("defectsIndex");
          } else {
            log.debug("response", response.status);
          }
        }
      ); 
    } else {
      alert("Stop hacking!");
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
    this.defectsService.fetch(params.id).then(
      defect => {
        log.debug("defect", defect);
        this.defect = defect;
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
