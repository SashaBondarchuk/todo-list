import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { TaskService } from 'src/app/core/services/task.service';
import { ITask } from 'src/app/shared/models/task/ITask';
import { IUpdateTask } from 'src/app/shared/models/task/IUpdateTask';
import { INewTask } from 'src/app/shared/models/task/INewTask';

@Component({
    selector: 'app-task-dialog',
    templateUrl: './task-dialog.component.html',
    styleUrls: ['./task-dialog.component.scss']
})
export class TaskDialogComponent {
    form: FormGroup;

    constructor(
        private fb: FormBuilder,
        private dialogRef: MatDialogRef<TaskDialogComponent>,
        private taskService: TaskService,
        @Inject(MAT_DIALOG_DATA) public data: { task: ITask, isNew: boolean }
    ) {
        this.form = this.fb.group({
            title: [data.task.title, Validators.required],
            description: [data.task.description, Validators.required],
        });
    }

    onCancel() {
        this.dialogRef.close();
    }

    onSave() {
        if (this.form.invalid) {
            return;
        }

        const taskData = {
            title: this.form.value.title,
            description: this.form.value.description,
            isCompleted: this.data.isNew ? false : this.data.task.isCompleted,
        };

        const taskObservable = this.data.isNew ?
            this.taskService.createTask(taskData) :
            this.taskService.editTask({ ...taskData, id: this.data.task.id });

        taskObservable.subscribe((result) => {
            this.dialogRef.close(result);
        });
    }
}