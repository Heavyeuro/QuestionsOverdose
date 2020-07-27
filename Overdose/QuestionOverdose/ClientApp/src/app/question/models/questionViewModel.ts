import { AnswerModel } from './';

export class QuestionViewModel {
  id: number;

  title: string;

  body: string;

  authorName: string;

  isAnswered: boolean;

  answerModels: AnswerModel[];

  dateOfPublication = new Date();
}
