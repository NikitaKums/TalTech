import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IDefect} from "../interfaces/IDefect";
import {DefectsService} from "../services/defects-service";
import {IAppUser} from "../interfaces/IAppUser";
import {AppUserService} from "../services/app-user-service";

export var log = LogManager.getLogger('Defects.Create');

@autoinject
export class Create {
  
  private defect: IDefect; 
  private appUser: IAppUser;
  
  constructor(private router: Router,
              private defectsService: DefectsService,
              private appUserService: AppUserService) {
    log.debug('constructor');
  }

  // ============ View Methods ============
  submit():void{
    log.debug('defect', this.defect);
    if (this.defect == undefined || this.defect.description.trim().length == 0){
      alert("Defect description cant be empty!");
      return;
    }
    this.defect.shopId = this.appUser.shopId;
    this.defectsService.post(this.defect).then(
      response => {
        if (response.status == 201){
          this.router.navigateToRoute("defectsIndex");
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

    this.appUserService.getUserShopId().then(
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
