import { Component, OnInit, Input, AfterViewInit } from '@angular/core';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.scss']
})
export class ToastComponent implements AfterViewInit {

  @Input() errorC?: string;
  @Input() errorMs?: string[];

  constructor(private toastService: ToastService) {
    toastService.error.subscribe(errorContent => {
      this.errorC = errorContent.errorCode;
      this.errorMs = errorContent.errorMessages;
      this.trigger();
    });
  }

  ngAfterViewInit(): void {
    this.trigger();
  }

  trigger() {
    if (this.errorC !== undefined && this.errorMs !== undefined) {
      const x = document.getElementById('snackbar');

      x.className = 'show';

      setTimeout(() => {
        x.className = x.className.replace('show', '');
        this.errorC = undefined;
        this.errorMs = undefined;
      }, 10000);
    }
  }

}
