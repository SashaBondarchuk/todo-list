import { Component, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
    form: FormGroup = new FormGroup({
        username: new FormControl('', [Validators.required]),
        password: new FormControl('', [Validators.required]),
    });

    formInvalid = true;

    error!: string | null;

    @Output() submitEM = new EventEmitter();

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
    }
}