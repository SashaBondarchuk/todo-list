import { Component, OnInit, Inject, EventEmitter } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { takeUntil } from 'rxjs/operators';
import { BaseComponent } from 'src/app/core/base/base.component';
import { TaskService } from 'src/app/core/services/task.service';

@Component({
    selector: 'app-delete-dialog',
    templateUrl: './delete-dialog.component.html',
})
export class DeleteDialogComponent extends BaseComponent implements OnInit {
    title: string = '';

    content: string = '';

    taskDeleted = new EventEmitter<number>();

    constructor(
        private dialogRef: MatDialogRef<DeleteDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any,
        private taskService: TaskService,
        private toastrService: ToastrService) {
        super();
    }

    public close() {
        this.dialogRef.close(false);
    }

    deleteTask() {
        this.taskService.deleteTask(this.data.entity.id)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: () => {
                    this.dialogRef.close(this.data.entity.id);
                    this.taskDeleted.emit(this.data.entity.id);
                },
                error: () => this.toastrService.error("Error while deleting task")
            });
    }

    ngOnInit(): void {
        this.title = this.data.title;
        this.content = this.data.content;
    }
}
