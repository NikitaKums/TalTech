import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IShop} from "../interfaces/IShop";
import {IComment} from "../interfaces/IComment";
import {CommentsService} from "../services/comments-service";
import {ProductsService} from "../services/products-service";
import {IIdAndNameProduct} from "../interfaces/IIdAndNameProduct";

export var log = LogManager.getLogger('Comments.Edit');

@autoinject
export class Edit {

  private comment: IComment;
  private shop: IShop;
  private products: IIdAndNameProduct[] = [];

  constructor(private router: Router,
              private commentsService: CommentsService,
              private productsService: ProductsService) {
    log.debug('constructor');
  }
  // ============ View methods ==============
  submit():void{
    log.debug('comment', this.comment);
    if (this.comment == undefined || this.comment.commentTitle == undefined || this.comment.commentBody == undefined || 
        this.comment.commentBody.trim().length == 0 || this.comment.commentBody.trim().length == 0){
      alert("Comment body nor title can be empty!");
      return;
    }
    this.comment.shopId = this.shop.id;
    this.commentsService.put(this.comment!).then(
      response => {
        if (response.status == 204){
          this.router.navigateToRoute("commentsIndex");
        } else {
          alert("Invalid data entered");
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
    this.commentsService.fetch(params.id).then(
      comment => {
        log.debug('comment', comment);
        this.comment = comment;
      });

    this.productsService.fetchForProductsIdAndName().then(
      jsonData => {
        log.debug('productJsonData', jsonData);
        this.products = jsonData;
      }
    );
    
    this.commentsService.fetchForShop().then(
      shop => {
        this.shop = shop
      });
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}
