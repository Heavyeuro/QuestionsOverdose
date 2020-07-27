export class CommentModel {
  id: number;

  body: string;

  authorName: string;

  commentChilds?: CommentModel[];

  dateOfPublication: Date = new Date();

  // variable that indicates demand to display form
  displayForm: boolean = false;
}
