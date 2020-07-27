import { VotingModel } from './votingModel';

export class Question {
  id: number;

  title: string;

  body: string;

  votes: number = 0;

  views: number = 0;

  isAnswered: boolean = false;

  isUpvoted?: boolean;

  authorName: string;

  tagNames: string[];

  dateOfPublication = new Date();

  votingModel: VotingModel;
}
