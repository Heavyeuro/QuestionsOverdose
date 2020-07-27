import { Component, Input, EventEmitter, Output } from '@angular/core';

import { VotingModel } from '../../_models'

@Component({
  selector: 'vote-feature',
  templateUrl: 'vote-feature.component.html',
  styleUrls: ['vote-feature.component.css']
})
export class VoteFeatureComponent {
  @Input() votingModel: VotingModel; 
  @Output() newVotesNumber = new EventEmitter<VotingModel>();

  vote(isUpVote: boolean) {
    this.votingModel.isDisabled = true;
    this.votingModel.isUpvote = isUpVote;
    this.votingModel.votes = isUpVote ? this.votingModel.votes + 1 : this.votingModel.votes - 1;
    this.newVotesNumber.emit(this.votingModel);
  }
}
