import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private userData: string | null = null;

    public username: string | null = null;

    constructor() {
        this.userData = localStorage.getItem('userData');
        this.username = localStorage.getItem('username');
    }

    isAuthorized() {      
        return !!this.userData;
    };

    getUserData(): string | null {
        return this.userData;
    };

    setUserData(username: string, password: string) {
        const userData = btoa(`${username}:${password}`);

        localStorage.setItem('userData', userData);
        localStorage.setItem('username', username);

        this.userData = userData;
        this.username = username;
    }

    clearUserData() {       
        localStorage.removeItem('userData');

        this.userData = null;
        this.username = null;
    }
}