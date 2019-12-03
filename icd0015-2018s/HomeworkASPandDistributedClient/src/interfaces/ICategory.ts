import {ICategoryIdAndName} from "./ICategoryIdAndName";

export interface ICategory extends ICategoryIdAndName{
  shopId: number,
  categoryProductCount: number
}
