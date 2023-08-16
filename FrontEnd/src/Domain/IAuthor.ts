import type { IBook } from "@/Domain/IBook";

export interface IAuthor{
  id?: string,

  name: string,
  description: string,
  books?: IBook[]
}