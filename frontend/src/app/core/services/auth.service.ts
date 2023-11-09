import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    private readonly userDataKey = 'userData';
    private readonly usernameKey = 'username';

    private userData: string | null = null;
    public username: string | null = null;

    constructor() {
        this.loadUserDataFromLocalStorage();
    }

    isAuthorized(): boolean {
        return !!this.userData;
    }

    getUserData(): string | null {
        this.loadUserDataFromLocalStorage();
        return this.userData;
    }

    setUserData(username: string, password: string): void {
        const userData = btoa(`${username}:${password}`);
        localStorage.setItem(this.userDataKey, userData);
        localStorage.setItem(this.usernameKey, username);
        this.updateUserData(userData, username);
    }

    clearUserData(): void {
        localStorage.removeItem(this.userDataKey);
        localStorage.removeItem(this.usernameKey);
        this.updateUserData(null, null);
    }

    private loadUserDataFromLocalStorage(): void {
        this.userData = localStorage.getItem(this.userDataKey);
        this.username = localStorage.getItem(this.usernameKey);
    }

    private updateUserData(userData: string | null, username: string | null): void {
        this.userData = userData;
        this.username = username;
    }
}
