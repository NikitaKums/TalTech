import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IDefect} from "../interfaces/IDefect";
import {DefectsService} from "../services/defects-service";
import {ProductsWithDefectService} from "../services/products-with-defect-service";
import {IProductWithDefect} from "../interfaces/IProductWithDefect";

export var log = LogManager.getLogger('Defects.Details');

@autoinject
export class Details {
  
  private defect: IDefect;
  private productsWithDefect: IProductWithDefect[] = [];

  constructor(private router: Router,
              private defectsService: DefectsService,
              private productsWithDefectService: ProductsWithDefectService) {
    log.debug('constructor');
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

    this.productsWithDefectService.fetchForInfoByDefectId(params.id).then(
      productWithDefect => {
        log.debug("productInOrder", productWithDefect);
        this.productsWithDefect = productWithDefect;
        for (let productWithDefect of this.productsWithDefect){
          productWithDefect.dateString = productWithDefect.defectRecordingTime.toString().replace("T", " ");
        }
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
