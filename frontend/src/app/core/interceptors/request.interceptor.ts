import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Observable } from 'rxjs';

@Injectable()
export class RequestInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService) { }

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        const userData = this.authService.getUserData();

        if (!userData) {
            return next.handle(request);
        }

        const requestWithAuth = request.clone({
            headers: request.headers.set('Authorization', `Basic ${userData}`),
        });

        return next.handle(requestWithAuth);
    }
}