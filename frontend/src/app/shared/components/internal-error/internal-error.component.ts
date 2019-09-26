import { Component, OnInit } from '@angular/core';
import { InternalErrorService } from '../../services/internal-error.service';

@Component({
  selector: 'app-internal-error',
  template: `<div [innerHtml]="internalErrorPage"></div>`
})
export class InternalErrorComponent implements OnInit {

  private internalErrorPage = '';

  constructor(private internalErrorService: InternalErrorService) {
    this.internalErrorService.htmlTemplate.subscribe(html => {
      console.log(html);
      console.log('html');
      this.internalErrorPage = html;
    });
  }

  ngOnInit() {
  }

}
