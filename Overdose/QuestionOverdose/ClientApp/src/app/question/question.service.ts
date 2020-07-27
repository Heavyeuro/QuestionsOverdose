import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { GlobalConstants } from '../common/global-constants';
import { QuestionViewModel, AnswerModel, CommentTransferModel, CommentModel } from './models';
import { Question }from '../_models'

@Injectable({ providedIn: 'root' })
export class QuestionService {
  constructor(private http: HttpClient) {}

  createQuestion(question: Question) {
    return this.http.post<any>(GlobalConstants.apiURL + "question/add",
      JSON.stringify(question),
      {
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      });
  }

  getQuestion(id: number) {
    let params = new HttpParams().set("id", String(id));
    return this.http.get<QuestionViewModel>(GlobalConstants.apiURL + "question/getQuestion",
      {
        params: params,
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      });
  }

  voteAnswer(id: number, isUpvote: boolean) {
    let params = new HttpParams().set("id", String(id)).set("isUpvote", String(isUpvote));
    return this.http.get<any>(GlobalConstants.apiURL + "question/voteAnswer",
      {
        params: params,
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      });
  }

  createAnswer(questionId: number, answerBody: string) {
    let params = new HttpParams().set("questionId", String(questionId))
      .set("answerBody", String(answerBody));
    return this.http.post<AnswerModel>(GlobalConstants.apiURL + "question/answer",
      {},
      {
        params: params,
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      });
  }

  createComment(comment: CommentTransferModel) {
    let params = new HttpParams().set("answerId", String(comment.answerId))
      .set("body", comment.body).set("commentAncestorId", String(comment.commentAncestorId || 0));

    return this.http.post<CommentModel>(GlobalConstants.apiURL + "question/comment",
      {},
      {
        params: params,
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      });
  }

  markAsAnswer(answerId: number) {
    const params = new HttpParams().set("answerId", String(answerId));
    return this.http.post<AnswerModel>(GlobalConstants.apiURL + "question/mark/answer",
      {},
      {
        params: params,
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      });
  }

  deleteAnswer(answerId: number) {
    const params = new HttpParams().set("answerId", String(answerId));
    return this.http.post(GlobalConstants.apiURL + "question/delete/answer",
      {},
      {
        params: params,
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      });
  }

  updateAnswer(answerId: number, body: string) {
    const params = new HttpParams().set("answerId", String(answerId)).set("body", body);
    return this.http.post(GlobalConstants.apiURL + "question/update/answer",
      {},
      {
        params: params,
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      });
  }

  deleteComment(commentId: number) {
    const params = new HttpParams().set("commentId", String(commentId));
    return this.http.post(GlobalConstants.apiURL + "question/delete/comment",
      {},
      {
        params: params,
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      });
  }

  updateComment(commentId: number, body: string) {
    const params = new HttpParams().set("commentId", String(commentId)).set("body", body);
    return this.http.post(GlobalConstants.apiURL + "question/update/comment",
      {},
      {
        params: params,
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      });
  }
}
