import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';

import { Question } from "../_models/question";
import { GlobalConstants } from "../common/global-constants"

@Injectable({
  providedIn: 'root'
})
export class LiveSearchService {
  constructor(private http: HttpClient) {}

  fetchQuestions(titleName: string): Observable<Question[]> {
    let params = new HttpParams().set("titleName", String(titleName));
    return this.http.get<Question[]>(GlobalConstants.apiURL + "question/getQuestions/live",
      {
        params: params,
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      }).pipe(
      catchError(err => of([]))
    );
  }

  liveSearch<T, TR>(
    dataCb: (query: T) => Observable<TR>,
    delay = 250
  ) {
    return (source$: Observable<T>) => source$.pipe(
      debounceTime(delay),
      distinctUntilChanged(),
      switchMap(dataCb)
    );
  }
}
