import { BaseService } from "@/Services/BaseService";
import type { IBook } from "@/Domain/IBook";
import type { ITag } from "@/Domain/ITag";

export class TagService extends BaseService<ITag> {
  constructor() {
    super("tags");
  }
}
