import { NgModule } from '@angular/core';
import { BaseComponent } from './base/base.component';
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from '../shared/shared.module';

@NgModule({
    declarations: [
        BaseComponent
    ],
    imports: [
        HttpClientModule, SharedModule
    ]
})
export class CoreModule { }
