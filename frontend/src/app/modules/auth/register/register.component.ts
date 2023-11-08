import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { passwordMatchValidator } from 'src/app/shared/utils/validation/form-validator.helper';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
    formInvalid = true;

    error!: string | null;

    @Output() submitEM = new EventEmitter();

    form: FormGroup = new FormGroup({
        email: new FormControl('', [Validators.required]),
        username: new FormControl('', [Validators.required]),
        password: new FormControl('', [Validators.required]),
        confirmPassword: new FormControl('', [Validators.required, passwordMatchValidator]),
    });

    onSubmit() {
        if (!this.form.valid) {
            return;
        }

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