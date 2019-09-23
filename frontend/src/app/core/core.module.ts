import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { AuthenticationPageComponent } from './auth/pages/authentication-page/authentication-page.component';
import { AuthenticationComponent } from './auth/components/authentication/authentication.component';
import { CoreRoutingModule } from './core-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { RegistrationComponent } from './auth/components/registration/registration.component';
import { CommonModule } from '@angular/common';

@NgModule({
    declarations: [
        AuthenticationPageComponent,
        AuthenticationComponent,
        RegistrationComponent
    ],
    imports: [
        FormsModule,
        ReactiveFormsModule,
        CoreRoutingModule,
        HttpClientModule,
        CommonModule
    ],
    providers: []
})
export class CoreModule {

}
