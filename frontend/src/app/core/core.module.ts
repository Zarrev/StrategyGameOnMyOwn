import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { AuthenticationPageComponent } from './auth/pages/authentication-page/authentication-page.component';
import { AuthenticationComponent } from './auth/components/authentication/authentication.component';

@NgModule({
    declarations: [
        AuthenticationPageComponent,
        AuthenticationComponent
    ],
    imports: [
        FormsModule,
        ReactiveFormsModule
    ],
    providers: []
})
export class CoreModule {

}
