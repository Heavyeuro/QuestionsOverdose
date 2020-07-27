import { Component } from '@angular/core';
import { Subject } from 'rxjs';

import { LiveSearchService } from '../live-search.service';

@Component({
  selector: 'app-question-search',
  templateUrl: './live-search.component.html',
  styleUrls: ['./live-search.component.css']
})
export class QuestionLiveSearchComponent {
  private titleSubject = new Subject<string>();
  currentTitle = '';
  showResult = true;

  readonly questions$ = this.titleSubject.pipe(
    this.searchService.liveSearch(title => this.searchService.fetchQuestions(title))
  );

  constructor(private searchService: LiveSearchService) { }

  searchQuestions(currentTitle: string) {
    this.currentTitle = currentTitle;
    this.titleSubject.next(this.currentTitle);
    this.showResult = true;
  }

  onLinkClick() {
    this.currentTitle = "";
    this.showResult = false;
  }
}
