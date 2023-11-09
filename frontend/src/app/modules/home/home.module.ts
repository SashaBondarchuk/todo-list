import { NgModule } from '@angular/core';
import { HomePageComponent } from './home-page/home-page.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { TaskDialogComponent } from './task-dialog/task-dialog.component';

@NgModule({
    declarations: [
        HomePageComponent,
        DeleteDialogComponent,
        TaskDialogComponent
    ],
    imports: [
        SharedModule
    ],
    exports: [
        HomePageComponent
    ]
})
export class HomeModule { }
