import type { IBook } from "@/Domain/IBook";

export interface ICollect{
  id?: string,

  title: string,
  description: string,
  books: IBook[]
}