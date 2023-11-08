import { NgModule } from '@angular/core';
import { HomePageComponent } from './home-page/home-page.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
    declarations: [
        HomePageComponent
    ],
    imports: [
        SharedModule
    ],
    exports: [
        HomePageComponent
    ]
})
export class HomeModule { }
