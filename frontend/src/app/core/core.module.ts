import { NgModule } from '@angular/core';
import { BaseComponent } from './base/base.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { SharedModule } from '../shared/shared.module';
import { RequestInterceptor } from './interceptors/request.interceptor';

@NgModule({
    declarations: [
        BaseComponent
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: RequestInterceptor, multi: true },
    ],
    imports: [
        HttpClientModule, SharedModule
    ]
})
export class CoreModule { }
