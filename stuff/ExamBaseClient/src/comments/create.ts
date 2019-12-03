import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction, Router} from "aurelia-router";
import {IComment} from "../interfaces/IComment";
import {CommentsService} from "../services/comments-service";

export var log = LogManager.getLogger('Comments.Create');

@autoinject
export class Create {

  private comment: IComment;

  constructor(private router: Router,
              private commentsService: CommentsService) {
    log.debug('constructor');
  }

  // ============ View Methods ============
  submit():void{
    log.debug('comment', this.comment);
    if (this.comment == undefined || this.comment.commentTitle == undefined || this.comment.commentBody == undefined ||
      this.comment.commentBody.trim().length == 0 || this.comment.commentBody.trim().length == 0){
      alert("Comment body nor title can be empty!");
      return;
    }
    this.commentsService.post(this.comment).then(
      response => {
        if (response.status == 201){
          this.router.navigateToRoute("commentsIndex");
        } else {
          alert("Invalid data entered");
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
