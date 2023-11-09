import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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

    form: FormGroup = new FormGroup({
        email: new FormControl('', [Validators.required]),
        username: new FormControl('', [Validators.required]),
        password: new FormControl('', [Validators.required]),
        confirmPassword: new FormControl('', [Validators.required, passwordMatchValidator]),
    });

    constructor(
        private authService: AuthService,
        private userService: UserService,
        private router: Router,
        private toastrService: ToastrService) {
        super();
    }

    onSubmit() {
        if (this.form.invalid) {
            return;
        }

        const { username, password, email } = this.form.value;
        const newUser: INewUser = { username, password, email };

        this.userService.createUser(newUser)
            .pipe(takeUntil(this.unsubscribe$))
            .subscribe({
                next: (user: IUser) => {
                    this.authService.setUserData(username, password);
                    this.router.navigate(['']);
                    this.toastrService.success(`Welcome, ${user.username}!`);
                },
                error: (error) => this.toastrService.error(error.error.error),
            });

        this.resetFormState();
    }

    onTouched(field: string) {
        const isPasswordMatch = this.form.get('password')?.value === this.form.get('confirmPassword')?.value;

        this.formInvalid = this.form.get(field)?.touched && !this.form.get(field)?.valid || !isPasswordMatch;
        this.error = isPasswordMatch ? '' : 'Passwords do not match';
    }

    private resetFormState() {
        this.error = '';
        this.formInvalid = false;
    }
}