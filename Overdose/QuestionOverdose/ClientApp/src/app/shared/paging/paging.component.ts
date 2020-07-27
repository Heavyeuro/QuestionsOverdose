import { Component, Input, Output, EventEmitter } from '@angular/core';

import { PagingHelper } from '../pagingHelper'
import { GlobalConstants } from "../../common/global-constants"

@Component({
  selector: 'app-paging',
  templateUrl: './paging.component.html'
})
export class PagingComponent {
  pageService: PagingHelper;
  @Input() pageNumber: number;
  @Input() totalItems: number;
  @Input() pageSize: number;
  @Output() pageToNavigate = new EventEmitter<number>();

  navigate(pageToNavigate: number) {
    this.pageService.pageModel.pageNumber = pageToNavigate;
    this.pageToNavigate.emit(pageToNavigate);
  }

  ngOnChanges() {
    const pageSize = this.pageSize || GlobalConstants.questionPageSize;
    this.pageService = new PagingHelper(
      this.pageNumber,
      this.totalItems,
      pageSize);
  }
}
