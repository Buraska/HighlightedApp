import type { IFontFace } from "@/Domain/IFontFace";

export interface IPreference{

  id?: string,
  fontSize: number,
  fontFace: IFontFace,

}