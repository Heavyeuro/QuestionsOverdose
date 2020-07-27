import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { GlobalConstants } from '../common/global-constants';
import { User } from '../_models';
import { Profile } from './models'

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private http: HttpClient) { }

  getProfile(user: User) {
    return this.http.post<any>(GlobalConstants.apiURL + "auth/profile",
      JSON.stringify(user),
      {
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      });
  }

  profilePostRequest(path: string, obj: Profile) {
    let fullPath = GlobalConstants.apiURL + "auth" + path;
    return this.http.post<any>(fullPath, JSON.stringify(obj),
      {
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      });
  }
}
