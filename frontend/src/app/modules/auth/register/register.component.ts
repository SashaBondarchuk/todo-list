import { Component, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { takeUntil } from 'rxjs';
import { BaseComponent } from 'src/app/core/base/base.component';
import { AuthService } from 'src/app/core/services/auth.service';
import { UserService } from 'src/app/core/services/user.service';
import { INewUser } from 'src/app/shared/models/user/INewUser';
import { IUser } from 'src/app/shared/models/user/IUser';
import { passwordMatchValidator } from 'src/app/shared/utils/validation/form-validator.helper';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss']
})
export class RegisterComponent extends BaseComponent {
    formInvalid = true;

    error!: string | null;

    @Output() submitEM = new EventEmitter();

    form: FormGroup = new FormGroup({
        email: new FormControl('', [Validators.required]),
        username: new FormControl('', [Validators.required]),
        password: new FormControl('', [Validators.required]),
        confirmPassword: new FormControl('', [Validators.required, passwordMatchValidator]),
    });

    constructor(
        private authService: AuthService,
        private userService: UserService,
        private router: Router) 
    {
        super();
    }

    onSubmit() {
        if (!this.form.valid) {
            return;
        }

        const { username, password, email } = this.form.value;
        const newUser: INewUser = {
            username: username,
            password: password,
            email: email
        };

        this.userService.createUser(newUser)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: (response: IUser) => {
                    this.authService.setUserData(username, password);
                    this.router.navigate(['/home']);
                },
                error: (error) => {
                    console.error('Error creating user:', error);
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

        if (this.form.get('password')?.value === this.form.get('confirmPassword')?.value) {
            this.submitEM.emit(this.form.value);
            this.error = '';
            this.formInvalid = false;
        } else {
            this.formInvalid = true;
            this.error = 'Passwords do not match';
        }
    }
}