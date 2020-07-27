import { Component, Input, Output, EventEmitter } from "@angular/core";
import { CommentModel, AnswerModel, CommentTransferModel } from "../models";
import { FormGroup, FormControl, Validators } from "@angular/forms";

@Component({
  selector: "comment-form",
  templateUrl: "./comment-form.component.html"
})
export class CommentFormComponent {
  @Input() answerInput: AnswerModel;
  @Input() commentInput: CommentModel;
  @Output() formValues = new EventEmitter<CommentTransferModel>();
  bodyForm: FormGroup;

  constructor() {
    this.bodyForm = new FormGroup({
      "Body": new FormControl("Text body", Validators.required),
    });
  }

  submit() {
    let comment = new CommentTransferModel();
    comment.body = this.bodyForm.value["Body"];
    if (this.commentInput != null)
      comment.commentAncestorId = this.commentInput.id;
    comment.answerId = this.answerInput.id;
    this.formValues.emit(comment);
  }
}
