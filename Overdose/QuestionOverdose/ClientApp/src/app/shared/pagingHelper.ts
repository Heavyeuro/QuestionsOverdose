import { PageModel} from '../_models';

export class PagingHelper {
  pageSize: number ;
  pageModel = new PageModel();
  dispForm: boolean;

  constructor(pageNumber: number, totalItems: number, pageSize: number) {
    this.pageSize = pageSize;
    this.pageModel.pageNumber = pageNumber;
    this.pageModel.totalItems = totalItems;
    this.pageModel.totalPages = Math.ceil(totalItems / this.pageSize);
    this.dispForm = !(this.pageModel.totalPages === 1 || this.pageModel.totalPages === 0);
  }

  hasPreviousPage(): boolean {
    return (this.pageModel.pageNumber > 1);
  }

  hasNextPage(): boolean {
    return (this.pageModel.pageNumber < this.pageModel.totalPages-1);
  }

  dispFirstPage(): boolean {
    return this.pageModel.pageNumber >2;
  }

  dispLastPage(): boolean {
    return this.pageModel.totalPages !== 1 &&
      this.pageModel.pageNumber !== this.pageModel.totalPages;
  }

  dispLeftDots(): boolean {
    return this.pageModel.pageNumber > 3;
  }

  dispRightDots(): boolean {
    return this.pageModel.pageNumber < this.pageModel.totalPages-2;
  }
}
