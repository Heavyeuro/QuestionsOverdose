import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { AuthenticationService } from 'src/app/_services';
import { QuestionService } from "../question.service"
import { User, VotingModel } from "src/app/_models";
import { CommentModel, AnswerModel, CommentTransferModel, QuestionViewModel } from '../models'

@Component({
  templateUrl: 'question.component.html',
  styleUrls: ['question.component.css']
})
export class QuestionComponent implements OnInit {
  questionModel: QuestionViewModel = new QuestionViewModel;
  currentUser: User;
  showAnswerForm: boolean = false;

  constructor(
    private authenticationService: AuthenticationService,
    private route: ActivatedRoute,
    private questionService: QuestionService) {
  }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get("id");
    this.currentUser = this.authenticationService.currentUserValue;
    this.questionService.getQuestion(+id).subscribe(x => {
      this.questionModel = x;
      this.questionModel.answerModels.forEach(y => y.votingModel =
        new VotingModel(y.id, y.votes, this.currentUser != null, y.isUpvoted));
    });
  }

  onVote(newVotesNumber: VotingModel) {
    this.questionService.voteAnswer(newVotesNumber.id, newVotesNumber.isUpvote)
      .subscribe(x => x);
  }

  commentFormShow(comment: CommentModel) {
    if (this.currentUser)
      this.questionModel.answerModels
        .find(x => x.comments
          .find(y => y.id === comment.id).displayForm = true);
  }

  answerFormShow(answer: AnswerModel) {
    if (this.currentUser)
      this.questionModel.answerModels
        .filter(x => x.id === answer.id)
        .forEach(y => y.displayForm = true);
  }

  makeHighlight(highlight: boolean): boolean {
    return this.currentUser && highlight;
  }

  addAnswer() {
    if (this.currentUser)
      this.showAnswerForm = true;
  }

  onSubmit(formValues: [number, string]) {
    this.showAnswerForm = false;
    this.questionService.createAnswer(formValues[0], formValues[1]).subscribe(x => {
      x.votingModel = new VotingModel(x.id, x.votes, this.currentUser != null, x.isUpvoted);
      this.questionModel.answerModels.push(x);
    });
  }

  canMarkAsAnswer(): boolean {
    if (!this.currentUser)
      return false;
    return (this.currentUser.nickname === this.questionModel.authorName) && !this.questionModel.isAnswered;
  }

  markAsAnswer(answerId: number) {
    this.questionService.markAsAnswer(answerId).subscribe(null,
      null,
      () => {
        this.questionModel.isAnswered = true;
        this.questionModel.answerModels.find(x => x.id === answerId).isAnswer = true;
      });
  }

  // request to save in server new comment, displaying in case of successful executing
  onRequest(formValues: CommentTransferModel) {
    // TODO: some input check
    this.questionService.createComment(formValues).subscribe(x => {
      x.commentChilds = new Array();
      x.displayForm = false;
      //pushing to Array of child`s comments or comments 
      if (formValues.commentAncestorId == null) {
        this.questionModel.answerModels
          .find(y => y.id === formValues.answerId).comments.push(x);
      } else {
        this.questionModel.answerModels
          .find(y => y.id === formValues.answerId).comments
          .find(z => z.id === formValues.commentAncestorId).commentChilds.push(x);
      }
    });
  }

  isAdmin(): boolean {
    return this.currentUser ? this.currentUser.role === "Admin" : false;
  }

  isRedactor(): boolean {
    return this.currentUser ? this.currentUser.role === "Redactor" : false;
  }

  onDeleteAnswer(answerId: number) {
    this.questionService.deleteAnswer(answerId).subscribe();
    //if dropping current answer set appropriate flag that indicate existing of correct answer
    if (this.questionModel.answerModels.find(x => x.id !== answerId).isAnswer)
      this.questionModel.isAnswered = false;

    this.questionModel.answerModels = this.questionModel.answerModels.filter(x => x.id !== answerId);
  }

  onEditAnswer(answer: [number, string]) {
    this.questionService.updateAnswer(answer["0"], answer["1"]).subscribe();
    this.questionModel.answerModels.find(x => x.id === answer["0"]).body = answer["1"];
  }

  onDeleteComment(commentId: number) {
    this.questionService.deleteComment(commentId).subscribe();
    this.questionModel.answerModels
      .find(x => x.comments
        .find(y => y.id === commentId)).comments = this.questionModel.answerModels
      .find(x => x.comments
        .find(y => y.id === commentId)).comments
      .filter(y => y.id !== commentId);
  }

  onEditComment(comment: [number, string]) {
    this.questionService.updateComment(comment["0"], comment["1"]).subscribe();
    this.questionModel.answerModels.find(x => x.comments.find(y => y.id === comment["0"]).body = comment["1"]);
  }

  onDeleteSubComment(commentId: number) {
    this.questionService.deleteComment(commentId).subscribe();
    this.questionModel.answerModels.find(x => x.comments.find(
        y => y.commentChilds.find(z => z.id === commentId))).comments
      .find(x => x.commentChilds.find(y => y.id === commentId)).commentChilds =
      this.questionModel.answerModels.find(x => x.comments.find(
        y => y.commentChilds.find(z => z.id === commentId))).comments
      .find(x => x.commentChilds.find(y => y.id === commentId)).commentChilds.filter(z => z.id !== commentId);
  }

  onEditSubComment(comment: [number, string]) {
    this.questionService.updateComment(comment["0"], comment["1"]).subscribe();
    this.questionModel.answerModels.find(
      x => x.comments.find(y => y.commentChilds.find(z => z.id === comment["0"]).body = comment["1"]));
  }

  canEditAnswer(answerId: number): boolean {
    if (answerId == null || this.currentUser == null)
      return false;
    return this.questionModel.answerModels
      .find(x => x.id === answerId).authorName ===
      this.currentUser.nickname ||
      this.currentUser.role === "Admin";
  }

  canEditComment(commentId: number): boolean {
    if (commentId == null || this.currentUser == null)
      return false;
    return this.questionModel.answerModels
      .find(x => x.comments.find(y => y.id === commentId)).comments
      .find(x => x.id === commentId).authorName ===
      this.currentUser.nickname ||
      this.currentUser.role === "Admin";
  }
}
