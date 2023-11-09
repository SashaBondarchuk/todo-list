import { Injectable } from '@angular/core';
import { CustomHttpService } from './custom-http.service';
import { Observable } from 'rxjs';
import { ITask } from 'src/app/shared/models/task/ITask';
import { INewTask } from 'src/app/shared/models/task/INewTask';
import { IUpdateTask } from 'src/app/shared/models/task/IUpdateTask';

@Injectable({
    providedIn: 'root',
})
export class TaskService {
    private readonly baseUrl: string = '/tasks';

    constructor(private httpService: CustomHttpService) { }

    getUserTasks(): Observable<ITask[]> {
        return this.httpService.getRequest<ITask[]>(`${this.baseUrl}/userTasks`);
    }

    createTask(task: INewTask): Observable<ITask> {
        return this.httpService.postRequest<ITask>(`${this.baseUrl}/create`, task);
    }

    editTask(task: IUpdateTask): Observable<ITask> {
        return this.httpService.putRequest<ITask>(`${this.baseUrl}/edit`, task);
    }

    deleteTask(taskId: number): Observable<ITask> {
        return this.httpService.deleteRequest<ITask>(`${this.baseUrl}?taskId=${taskId}`);
    }
}