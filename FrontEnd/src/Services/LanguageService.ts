import { BaseService } from "@/Services/BaseService";
import type { IBook } from "@/Domain/IBook";
import type { ILanguage } from "@/Domain/ILanguage";

export class LanguageService extends BaseService<ILanguage> {
  constructor() {
    super("languages");
  }
}
