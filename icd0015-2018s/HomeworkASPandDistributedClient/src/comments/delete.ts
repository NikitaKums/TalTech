import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {CommentsService} from "../services/comments-service";
import {IComment} from "../interfaces/IComment";

export var log = LogManager.getLogger('Comments.Delete');

@autoinject
export class Delete {

  private comment: IComment;

  constructor(private router: Router,
              private commentsService: CommentsService) {
    log.debug('constructor');
  }

  // ============ View Methods ================
  submit():void{
    log.debug('submit', this.comment);
    
    this.commentsService.delete(this.comment.id).then(
      response => {
        if (response.status == 200){
          this.router.navigateToRoute("commentsIndex");
        } else {
          log.debug("response", response.status);
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
        log.debug("comment", comment);
        this.comment = comment;
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
