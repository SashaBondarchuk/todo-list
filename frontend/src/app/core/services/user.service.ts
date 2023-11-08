import { Injectable } from '@angular/core';
import { CustomHttpService } from './custom-http.service';
import { INewUser } from 'src/app/shared/models/user/INewUser';
import { IUser } from 'src/app/shared/models/user/IUser';
import { Observable } from 'rxjs';
import { IUserLogin } from 'src/app/shared/models/user/IUserLogin';

@Injectable({
    providedIn: 'root',
})
export class UserService {
    private readonly baseUrl: string = '/users';

    constructor(private httpService: CustomHttpService) { }

    createUser(newUser: INewUser): Observable<IUser> {
        return this.httpService.postRequest<IUser>(`${this.baseUrl}/register`, newUser);
    }

    login(user: IUserLogin): Observable<IUser> {
        return this.httpService.postRequest<IUser>(`${this.baseUrl}/login`, user);
    }
}