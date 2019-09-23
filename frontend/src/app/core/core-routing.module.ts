import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationPageComponent } from './auth/pages/authentication-page/authentication-page.component';
import { RegistrationComponent } from './auth/components/registration/registration.component';
import { AuthenticationComponent } from './auth/components/authentication/authentication.component';

const routes: Routes = [
    {
        path: '',
        component: AuthenticationPageComponent, children: [
            { path: '', redirectTo: 'login', pathMatch: 'full' },
            { path: 'registration', component: RegistrationComponent },
            { path: 'login', component: AuthenticationComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CoreRoutingModule { }
