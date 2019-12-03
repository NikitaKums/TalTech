import {IBaseEntity} from "./IBaseEntity";

export interface IProductWithDefect extends IBaseEntity {
  quantity: number,
  defectRecordingTime: Date,
  productId: number,
  productName: string,
  defectId: number,
  defectDescription: string,
  dateString: string
}
