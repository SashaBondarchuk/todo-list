import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { DeleteDialogComponent } from '../../modules/home/delete-dialog/delete-dialog.component';
import { ITask } from 'src/app/shared/models/task/ITask';

@Injectable({ providedIn: 'root' })
export class DeleteDialogService {

    constructor(private dialog: MatDialog) { }

    open<T extends ITask>(entity: T, title: string, content: string) {
        const dialogRef: MatDialogRef<DeleteDialogComponent> = this.dialog.open(DeleteDialogComponent, {
            data: { entity, title, content },
            minWidth: 300,
            autoFocus: true,
            backdropClass: 'dialog-backdrop',
        });

        return dialogRef.afterClosed();
    }
}
