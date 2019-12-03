import {IIdAndNameProduct} from "./IIdAndNameProduct";
import {ICategoryIdAndName} from "./ICategoryIdAndName";
import {ICommentIdTitleBody} from "./ICommentIdTitleBody";

export interface IProduct extends IIdAndNameProduct {
  manuFacturerItemCode: string,
  shopCode: string,
  productId: number,
  productName: string,
  buyPrice: number,
  percentageAddedToBuyPrice: number,
  sellPrice: number | null,
  quantity: number,
  weight: string,
  length: string,
  shopId: number,
  shopName: string,
  manuFacturerId: number,
  manuFacturerName: string,
  inventoryId: number,
  inventoryName: string,
  productsInOrdersCount: number,
  productsSoldCount: number,
  productReturnsCount: number,
  productsWithDefectCount: number,
  categoryDTOs: ICategoryIdAndName[],
  commentDTOs: ICommentIdTitleBody[]
}
