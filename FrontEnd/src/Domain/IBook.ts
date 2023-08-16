import type { IPreference } from "@/Domain/IPreference";
import type { IAuthor } from "@/Domain/IAuthor";
import type { ILanguage } from "@/Domain/ILanguage";
import type { ITag } from "@/Domain/ITag";
import type { ICollect } from "@/Domain/ICollect";
import type { IHighlighted } from "@/Domain/Highlighted";

export interface IBook{
  id?: string,
  title: string,
  symbolsTotal: number,
  currentSymbol: number,
  lastAccessAt?: string,
  isFinished: boolean,
  content: string

  preference?: IPreference,
  author?: IAuthor,
  language?: ILanguage,
  tags?: ITag[],
  collects?: ICollect[],
  highlighteds: IHighlighted[],


}