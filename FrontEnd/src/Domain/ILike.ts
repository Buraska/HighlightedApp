import type { IHighlighted } from "@/Domain/Highlighted";
import type { IAppUser } from "@/Domain/IAppUser";

export interface ILike{
  id?: string,

  highlighted: IHighlighted,
  appUser: IAppUser
}