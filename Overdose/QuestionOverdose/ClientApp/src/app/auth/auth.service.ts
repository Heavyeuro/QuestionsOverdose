import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { GlobalConstants } from '../common/global-constants';
import { User } from '../_models';

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private http: HttpClient) {}

  register(user: User) {
    return this.http.post<any>(GlobalConstants.apiURL + "auth/register",
      JSON.stringify(user),
      {
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      });
  }
}
