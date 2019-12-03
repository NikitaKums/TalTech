import {ICommentIdTitleBody} from "./ICommentIdTitleBody";

export interface IComment extends ICommentIdTitleBody{
  productName: string,
  productId: number,
  shopId: number
}
