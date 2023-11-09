import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { takeUntil } from 'rxjs';
import { BaseComponent } from 'src/app/core/base/base.component';
import { DeleteDialogService } from 'src/app/core/services/delete-dialog.service';
import { TaskDialogService } from 'src/app/core/services/task-dialog.service';
import { TaskService } from 'src/app/core/services/task.service';
import { ITask } from 'src/app/shared/models/task/ITask';
import { IUpdateTask } from 'src/app/shared/models/task/IUpdateTask';

@Component({
    selector: 'app-home-page',
    templateUrl: './home-page.component.html',
    styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent extends BaseComponent {
    tasks: ITask[] = [];

    constructor(
        private taskService: TaskService,
        private toastrService: ToastrService,
        private deleteDialogService: DeleteDialogService,
        private taskDialogService: TaskDialogService) {
        super();
    }

    ngOnInit(): void {
        this.loadTasks();
    }

    loadTasks() {
        this.taskService.getUserTasks()
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: (tasks: ITask[]) => {
                    this.tasks = tasks;
                },
                error: () => {
                    this.toastrService.error('Error while loading tasks');
                }
            });
    }

    onAddTask() {
        this.taskDialogService.openCreateTask().afterClosed().subscribe((result: ITask) => {
            if (result) {
                this.tasks.push(result);
            }
        });
    }

    onCheckboxChange(task: ITask) {
        const taskToUpdate: IUpdateTask = {
            ...task,
            isCompleted: !task.isCompleted,
        };

        this.taskService.editTask(taskToUpdate)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: (updatedTask) => {
                    let task = this.tasks.find(t => t.id === updatedTask.id)!;
                    task.isCompleted = updatedTask.isCompleted;
                },
                error: (error) => this.toastrService.error("Error while editing task: " + error.error.error)
            });
    }

    onEditTask(task: ITask) {
        this.taskDialogService.openEditTask(task).afterClosed()
        .pipe(takeUntil(this.unsubscribe$))
        .subscribe({
            next: (result: ITask) => {
                let editedTask = this.tasks.find(t => t.id === result.id);
                if (editedTask) {
                    editedTask.title = result.title;
                    editedTask.description = result.description;
                }
            },
            error: (error) => this.toastrService.error("Error while editing task: " + error.error.error)

        });
    }

    onDelete(task: ITask) {
        this.deleteDialogService.open(task, 'Delete task', 'Are you sure you want to delete this task?')
            .subscribe((deletedTaskId: number) => {
                this.tasks = this.tasks.filter(t => t.id !== deletedTaskId);
            });
    }
}
