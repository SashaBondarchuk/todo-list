import { Component, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { UserService } from 'src/app/core/services/user.service';
import { IUserLogin } from 'src/app/shared/models/user/IUserLogin';
import { takeUntil } from 'rxjs';
import { BaseComponent } from 'src/app/core/base/base.component';
import { IUser } from 'src/app/shared/models/user/IUser';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent extends BaseComponent {
    form: FormGroup = new FormGroup({
        username: new FormControl('', [Validators.required]),
        password: new FormControl('', [Validators.required]),
    });

    formInvalid = true;

    error!: string | null;

    @Output() submitEM = new EventEmitter();

    constructor(
        private userService: UserService,
        private authService: AuthService,
        private router: Router,
        private toastrService: ToastrService) {
        super();
    }

    onSubmit() {
        if (!this.form.valid) {
            return;
        }

        const { username, password } = this.form.value;

        const user: IUserLogin = {
            username: username,
            password: password,
        };

        this.userService.login(user)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: (response: IUser) => {
                    this.authService.setUserData(username, password);
                    this.router.navigate(['']);
                    this.toastrService.success(`Welcome, ${response.username}!`);
                },
                error: () => {                   
                    this.toastrService.error('Error while login, try again');
                }
            });

        this.submitEM.emit(this.form.value);
        this.error = '';
        this.formInvalid = false;
    }

    onTouched(field: string) {
        if (this.form.get(field)?.touched && !this.form.get(field)?.valid) {
            this.formInvalid = true;
        } else {
            this.formInvalid = false;
        }
    }
}