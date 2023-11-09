import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { takeUntil } from 'rxjs';
import { BaseComponent } from 'src/app/core/base/base.component';
import { DeleteDialogService } from 'src/app/core/services/delete-dialog.service';
import { TaskService } from 'src/app/core/services/task.service';
import { ITask } from 'src/app/shared/models/task/ITask';

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
        private deleteDialogService: DeleteDialogService) {
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

    onEdit(task: ITask) {

    }

    onDelete(task: ITask) {
        this.deleteDialogService.open(task, 'Delete task', 'Are you sure you want to delete this task?')
            .subscribe((deletedTaskId: number) => {
                this.tasks = this.tasks.filter(t => t.id !== deletedTaskId);
            });
    }
}
