import { VotingModel } from 'src/app/_models';
import { CommentModel } from './commentModel'

export class AnswerModel {
  id: number;

  votes: number;

  body: string;

  authorName: string;

  isAnswer: boolean;

  comments: CommentModel[];

  dateOfPublication: Date = new Date();

  isUpvoted?: boolean;

  votingModel: VotingModel;

  // variable that indicates demand to display form
  displayForm: boolean = false;
}
