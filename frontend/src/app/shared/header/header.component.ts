import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
    constructor(private authService: AuthService, private router: Router) { }

    username = this.authService.username;
    isAuthorized = this.authService.isAuthorized();

    ngOnInit() {
        this.router.events.subscribe((event) => {
            if (event instanceof NavigationEnd) {
                this.username = this.authService.username;
                this.isAuthorized = this.authService.isAuthorized();
            }
        });
    }

    logout() {
        this.authService.clearUserData();

        this.router.navigate(['/auth/login']);
    }
}
