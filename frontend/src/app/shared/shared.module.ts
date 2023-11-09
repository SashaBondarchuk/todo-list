import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { HeaderComponent } from './header/header.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
    declarations: [
        HeaderComponent
    ],
    imports: [
        CommonModule,
        MatToolbarModule,
        MatButtonModule,
    ],
    exports: [
        RouterModule,
        CommonModule,
        MatButtonModule,
        MatInputModule,
        MatCardModule,
        HeaderComponent,
        MatListModule,
        MatCheckboxModule,
        MatButtonModule,
        MatCardModule,
        MatInputModule,
        MatIconModule,
        MatDialogModule
    ]
})
export class SharedModule { }
