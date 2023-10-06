import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss'],
})
export class PaginationComponent {
  @Input() pageSize?: number;
  @Input() totalCount?: number;
  @Output() paginate = new EventEmitter<number>();

  onPageChanged(event: any) {
    this.paginate.emit(event.page);
  }
}
