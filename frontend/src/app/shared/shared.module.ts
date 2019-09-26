import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastComponent } from './components/toast/toast.component';
import { InternalErrorComponent } from './components/internal-error/internal-error.component';



@NgModule({
  declarations: [ToastComponent, InternalErrorComponent],
  imports: [
    CommonModule
  ],
  exports: [CommonModule, ToastComponent, InternalErrorComponent]
})
export class SharedModule { }
