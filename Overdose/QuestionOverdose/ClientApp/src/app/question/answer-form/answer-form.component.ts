import { Component, Input,Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { Question } from 'src/app/_models';

@Component({
  selector: 'answer-form',
  templateUrl: 'answer-form.component.html'
})
export class AnswerFormComponent {
  @Input() questionInput: Question;
  @Output() formValues = new EventEmitter<[number, string]>();
  bodyForm: FormGroup;

  constructor() {
    this.bodyForm = new FormGroup({
      "Body": new FormControl("Text body", Validators.required),
    });
  }

  submit() {
    this.formValues.emit([this.questionInput.id, this.bodyForm.value["Body"]]);
  }
}
