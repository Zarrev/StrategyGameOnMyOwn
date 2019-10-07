
import { NgModule } from '@angular/core';

import { CommonModule } from '@angular/common';
import { BuildingsRoutingModule } from './buildings-routing.module';
import { BuildingPagesComponent } from './pages/building-pages/building-pages.component';
import { BuildComponent } from './components/build/build.component';


@NgModule({
    declarations: [
        BuildingPagesComponent,
        BuildComponent
    ],
    imports: [
        CommonModule,
        BuildingsRoutingModule
    ]
})

export class BuildingsModule { }
