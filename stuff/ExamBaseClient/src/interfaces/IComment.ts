import {IBaseEntity} from "./IBaseEntity";

export interface IComment extends IBaseEntity{
  commentTitle: string,
  commentBody: string
}
