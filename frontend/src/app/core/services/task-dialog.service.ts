import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { TaskDialogComponent } from '../../modules/home/task-dialog/task-dialog.component';
import { ITask } from '../../shared/models/task/ITask';

@Injectable({ providedIn: 'root' })
export class TaskDialogService {

    constructor(private dialog: MatDialog) { }

    openCreateTask() {
        return this.openTaskDialog({ task: {} as ITask, isNew: true });
    }

    openEditTask(task: ITask) {
        return this.openTaskDialog({ task, isNew: false });
    }

    private openTaskDialog(data: { task: ITask, isNew: boolean }): MatDialogRef<TaskDialogComponent> {
        return this.dialog.open(TaskDialogComponent, {
            data,
            minWidth: 400,
            autoFocus: true,
            backdropClass: 'dialog-backdrop',
        });
    }
}