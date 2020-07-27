import { RoleEnum } from "src/app/_models";

export class Profile {

  fullName: string;

  email: string;

  username: string;

  password: string;

  isEmailVerified: boolean;

  subscribedTags: string[];

  role: RoleEnum;
}
