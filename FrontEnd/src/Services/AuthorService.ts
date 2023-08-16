import { BaseService } from "@/Services/BaseService";
import type { IAuthor } from "@/Domain/IAuthor";

export class AuthorService extends BaseService<IAuthor> {
  constructor() {
    super("authors");
  }
}
