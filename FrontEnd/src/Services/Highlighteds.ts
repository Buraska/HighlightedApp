import { BaseService } from "@/Services/BaseService";
import type { IBook } from "@/Domain/IBook";
import type { ILanguage } from "@/Domain/ILanguage";
import type { IHighlighted } from "@/Domain/Highlighted";

export class HighlightedsService extends BaseService<IHighlighted> {
  constructor() {
    super("highlighteds");
  }
}
