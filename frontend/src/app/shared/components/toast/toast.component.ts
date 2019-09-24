import { Component, OnInit, Input, AfterViewInit } from '@angular/core';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.scss']
})
export class ToastComponent implements AfterViewInit {

  @Input() errorC?: string;
  @Input() errorM?: string;
  private message: string;

  constructor(private toastService: ToastService) {
    this.message = this.errorC + ': ' + this.errorM;
    toastService.error.subscribe(msg => this.message = msg);
  }

  ngAfterViewInit(): void {
    this.trigger();
  }

  trigger() {
    if ((this.errorC !== undefined && this.errorM !== undefined) || this.message !== 'undefined: undefined') {
      const x = document.getElementById('snackbar');

      // Add the "show" class to DIV
      x.className = 'show';

      // After 3 seconds, remove the show class from DIV
      setTimeout(() => { x.className = x.className.replace('show', ''); }, 3000);
    }
  }

}
