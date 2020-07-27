import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

import { User, Question, VotingModel, Tag } from 'src/app/_models';
import { QuestionIndex } from "../models/questionIndex"
import { HomeService } from "../home.service";

@Component({
  selector: 'question-element',
  templateUrl: 'question-element.component.html'
})
export class QuestionElementComponent {
  @Input() currentUser: User;
  @Input() question: QuestionIndex;
  @Output() tag = new EventEmitter<Tag>();

  constructor(
    private questionService: HomeService,
    private router: Router
  ) {}

  onVote(newVotesNumber: VotingModel) {
    this.questionService.voteQuestion(newVotesNumber.id, newVotesNumber.isUpvote)
      .subscribe(x => x);
  }

  navigateOnTag(tag: Tag) {
    this.tag.emit(tag);
  }
}
