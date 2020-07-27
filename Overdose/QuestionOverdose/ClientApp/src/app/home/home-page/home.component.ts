import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { User, VotingModel } from 'src/app/_models';
import { QuestionIndex } from "../models/questionIndex";
import { AuthenticationService } from 'src/app/_services';
import { HomeService } from "../home.service"
import { GlobalConstants } from 'src/app/common/global-constants';
import { Question } from "../../_models/Question";

@Component({
  templateUrl: 'home.component.html',
})
export class HomeComponent implements OnInit {
  currentUser: User;
  questionModel: QuestionIndex = new QuestionIndex();
  isDownloaded: boolean = false;
  currentPage: number = 1;
  filteringTagName: string;

  constructor(
    private authenticationService: AuthenticationService,
    private questionService: HomeService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.currentUser = this.authenticationService.currentUserValue;
    this.route.queryParams.subscribe(params => {
      this.filteringTagName = params["tagName"];
      this.currentPage = params["page"] || this.currentPage;
      this.questionService.getQuestions(GlobalConstants.questionPageSize, this.currentPage, this.filteringTagName)
        .subscribe(x => {
          this.questionModel = x;
          this.isDownloaded = true;
          this.questionModel.questionModels.forEach(y => y.votingModel =
            new VotingModel(y.id, y.votes, this.currentUser != null, y.isUpvoted));
        });
    });
  }

  onNavigate(pageToNavigate: number) {
    this.route.queryParams.subscribe(params => {
      this.filteringTagName = params["tagName"];
      this.router.navigate(['/questions'], { queryParams: { page: pageToNavigate, tagName: this.filteringTagName } });
    });
  }

  onTagNavigate(tagName: string) {
    this.router.navigate(['/questions'], { queryParams: { tagName: tagName, page: 1 } });
  }

  isAdmin(): boolean {
    return this.currentUser ? this.currentUser.role === "Admin" : false;
  }

  canEdit(questionId: number): boolean {
    if (questionId == null || this.currentUser == null)
      return false;
    return this.questionModel.questionModels
        .find(x => x.id === questionId).authorName === this.currentUser.nickname
      || this.currentUser.role === "Admin";
  }

  onDeleteQuestion(questionId: number) {
    this.questionService.deleteQuestion(questionId).subscribe();
    this.questionModel.questionModels = this.questionModel.questionModels.filter(x => x.id !== questionId);
    this.questionModel.pageModel.totalItems--;
  }

  onEditQuestion(question: [number, string]) {
    this.questionService.updateQuestion(question["0"], question["1"]).subscribe();
    this.questionModel.questionModels.find(x => x.id === question["0"]).body = question["1"];
  }

  viewQuestion(question: Question) {
    this.router.navigate(["/question", question.id]);
  }
}
