import type { IBook } from "@/Domain/IBook";
import type { IHighlightedType } from "@/Domain/HighlightedType";
import type { ILike } from "@/Domain/ILike";

export interface IHighlighted{
  id?: string,
  comment?: string,
  content: string,
  orderNo: number,
  startAt: number,
  endAt: number,

  book?: IBook,
  bookId?: string,
  highlightedType: IHighlightedType,
  likes: ILike[]
}