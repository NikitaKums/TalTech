import {IBaseEntity} from "./IBaseEntity";

export interface ICommentIdTitleBody extends IBaseEntity{
  commentTitle: string,
  commentBody: string,
}
