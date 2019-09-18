import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { AuthenticationPageComponent } from './auth/pages/authentication-page/authentication-page.component';
import { AuthenticationComponent } from './auth/components/authentication/authentication.component';
import { CoreRoutingModule } from './core-routing.module';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
    declarations: [
        AuthenticationPageComponent,
        AuthenticationComponent
    ],
    imports: [
        FormsModule,
        ReactiveFormsModule,
        CoreRoutingModule,
        HttpClientModule
    ],
    providers: []
})
export class CoreModule {

}
