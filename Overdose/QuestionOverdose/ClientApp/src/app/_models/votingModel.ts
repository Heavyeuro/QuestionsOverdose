export class VotingModel {
  id: number;
  isUpvote?: boolean;
  votes: number;
  isDisabled: boolean;
  canVote: boolean;

  constructor(id: number, votes: number, canVote: boolean, isUpvote?: boolean) {
    this.id = id;
    this.votes = votes;
    this.isUpvote = isUpvote;
    this.isDisabled = !(isUpvote == null);
    this.canVote = canVote;
  }
}
