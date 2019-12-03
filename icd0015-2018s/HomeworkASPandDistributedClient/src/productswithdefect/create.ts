import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {ProductsWithDefectService} from "../services/products-with-defect-service";
import {IProductWithDefect} from "../interfaces/IProductWithDefect";
import {IDefect} from "../interfaces/IDefect";
import {DefectsService} from "../services/defects-service";
import {ProductsService} from "../services/products-service";
import {IIdAndNameProduct} from "../interfaces/IIdAndNameProduct";

export var log = LogManager.getLogger('ProductsWithDefect.Create');

@autoinject
export class Create {
  
  private productWithDefect: IProductWithDefect;
  private defects: IDefect[] = [];
  private products: IIdAndNameProduct[] = [];
  private selectedField: number;

  constructor(private router: Router,
              private defectsService: DefectsService,
              private productsService: ProductsService,
              private productsWithDefectService: ProductsWithDefectService) {
    log.debug('constructor');
  }

  // ============ View Methods ============
  submit():void{
    log.debug('productWithDefect', this.productWithDefect,);
    this.productWithDefect.defectRecordingTime.setMinutes(this.productWithDefect.defectRecordingTime.getMinutes() + 180);
    this.productWithDefect.id = 0;
    this.productWithDefect.defectId = this.selectedField;
    this.productsWithDefectService.post(this.productWithDefect).then(
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
    this.productsService.fetchForProductsIdAndName().then(
      jsonData => {
        log.debug("products", jsonData);
        this.products = jsonData;
      }
    );

    this.defectsService.fetchAll(undefined, undefined, undefined).then(
      jsonData => {
        log.debug("defects", jsonData);
        this.defects = jsonData;
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
    this.selectedField = params.id;
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
