import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './modules/home/home-page/home-page.component';
import { UnAuthorizedGuard } from './core/guards/unauthorized.guard';
import { AuthorizedGuard } from './core/guards/authorized.guard';

const routes: Routes = [
    {
        path: '',
        component: HomePageComponent,
        pathMatch: 'full',
        canActivate: [AuthorizedGuard],
    },
    {
        path: 'auth',
        loadChildren: () => import('./modules/auth/auth.module').then((m) => m.AuthModule),
        canActivate: [UnAuthorizedGuard],
    },
    { path: '**', component: HomePageComponent, pathMatch: 'full' },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
