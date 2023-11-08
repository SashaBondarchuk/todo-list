import { NgModule } from '@angular/core';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthRoutingModule } from './auth-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
    declarations: [
        LoginComponent,
        RegisterComponent
    ],
    imports: [
        SharedModule, AuthRoutingModule, ReactiveFormsModule
    ]
})
export class AuthModule { }
