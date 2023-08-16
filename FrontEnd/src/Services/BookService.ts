import { BaseService } from "@/Services/BaseService";
import type { IBook } from "@/Domain/IBook";

export class BookService extends BaseService<IBook> {
  constructor() {
    super("books");
  }
}
