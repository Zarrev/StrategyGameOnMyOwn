import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { AuthenticationPageComponent } from './auth/pages/authentication-page/authentication-page.component';
import { AuthenticationComponent } from './auth/components/authentication/authentication.component';
import { CoreRoutingModule } from './core-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { RegistrationComponent } from './auth/components/registration/registration.component';
import { SharedModule } from '../shared/shared.module';

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
        SharedModule
    ],
    providers: []
})
export class CoreModule {

}
