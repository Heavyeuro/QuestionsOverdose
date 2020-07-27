import { Component, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { ModalService } from "../../_modal/modal.service";
import { SimpleChanges } from "@angular/core/core";

@Component({
  selector: 'admin-controller',
  templateUrl: './admin-controller.component.html',
  styleUrls: ['./admin-controller.component.css']
})
export class AdminControllerComponent implements OnChanges {
  @Input() stringToRedact: string;
  @Input() objectId: number;
  @Output() toDeleteObjectId = new EventEmitter<number>();
  @Output() editedString = new EventEmitter<[number, string]>();
  modalId: string; 

  constructor(private modalService: ModalService) {}

  ngOnChanges(): void {
    this.modalId = this.stringToRedact + String(this.objectId);
  }

  toEdit(id: string) {
    this.editedString.emit([this.objectId, this.stringToRedact]);
    this.modalService.close(id);
  }

  toDelete() {
    this.toDeleteObjectId.emit(this.objectId);
  }

  openModal(id: string) {
    this.modalService.open(id);
  }

  closeModal(id: string) {
    this.modalService.close(id);
  }
}
