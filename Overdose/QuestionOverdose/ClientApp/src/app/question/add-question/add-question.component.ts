import { Component, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { of } from 'rxjs';

import { AuthenticationService, TagService} from 'src/app/_services'
import { User, Tag, Question } from 'src/app/_models';
import { QuestionService } from "../question.service"

@Component({
  selector: 'profile-component',
  templateUrl: './add-question.component.html'
})
export class AddQuestionComponent implements OnInit {

  form: FormGroup;
  currentUser: User;
  allTags: Tag[] = [];
  selectedItems = [];
  dropdownSettings: any = {};

  constructor(
    private authenticationService: AuthenticationService,
    private formBuilder: FormBuilder,
    private questionService: QuestionService,
    private router: Router,
    private tagService: TagService)
  { }

  ngOnInit(): void {
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
    this.form = this.formBuilder.group({
      "Title": new FormControl("Title", Validators.required),
      "Body": new FormControl("Body", Validators.required),
      selectedTags: [this.selectedItems]
    });

    of(this.tagService.getTags().subscribe(data => {
      this.allTags = data;
    }));

    this.dropdownSettings = {
      idField: 'id',
      textField: 'tagName',
      itemsShowLimit: 3,
      allowSearchFilter: true,
      limitSelection: 3
    };
  }

  submit() {
    const selectedTagNames = this.form.value.selectedTags
      .map((v, i) => v ? this.allTags[i].tagName : null)
      .filter(v => v !== null);

    this.addQuestion(selectedTagNames);
  }

  addQuestion(selectedTagNames: string[]) {
    let newQuestion = new Question();

    newQuestion.authorName = this.currentUser.nickname;
    newQuestion.title = this.form.value.Title;
    newQuestion.body = this.form.value.Body;
    newQuestion.tagNames = selectedTagNames;
    this.questionService.createQuestion(newQuestion).subscribe(null , null, () => this.router.navigate(['']));
  }
}
