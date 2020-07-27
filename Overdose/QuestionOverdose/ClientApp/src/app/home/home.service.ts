import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { QuestionIndex } from "./models/questionIndex";
import { GlobalConstants } from '../common/global-constants';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  constructor(private http: HttpClient) { }

  getQuestions(pageSize: number, pageNumber: number, tagName: string = "") {
    const params = new HttpParams().set("pageSize", String(pageSize))
      .set("pageNumber", String(pageNumber)).set("tagName", tagName);
    return this.http.get<QuestionIndex>(GlobalConstants.apiURL + "question/get",
      {
        params: params,
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      });
  }

  voteQuestion(id: number, isUpvote: boolean) {
    const params = new HttpParams().set("id", String(id)).set("isUpvote", String(isUpvote));
    return this.http.get<any>(GlobalConstants.apiURL + "question/voteQuestion", {
      params: params, headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }

  deleteQuestion(questionId: number) {
    const params = new HttpParams().set("questionId", String(questionId));
    return this.http.post(GlobalConstants.apiURL + "question/delete", {},{
      params: params, headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }

  updateQuestion(questionId: number, body: string) {
    const params = new HttpParams().set("questionId", String(questionId)).set("body", body);
    return this.http.post(GlobalConstants.apiURL + "question/update", {}, {
      params: params, headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }
}
