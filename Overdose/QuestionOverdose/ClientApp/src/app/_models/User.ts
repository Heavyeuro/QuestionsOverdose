import { RoleEnum } from "./roleEnum";

export class User {
  id: number;

  email: string;

  nickname: string;

  password: string;

  role: RoleEnum;

  token: string;
}
