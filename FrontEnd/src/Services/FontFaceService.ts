import { BaseService } from "@/Services/BaseService";
import type { IBook } from "@/Domain/IBook";
import type { ILanguage } from "@/Domain/ILanguage";
import type { IFontFace } from "@/Domain/IFontFace";

export class FontFaceService extends BaseService<IFontFace> {
  constructor() {
    super("fontfaces");
  }
}
