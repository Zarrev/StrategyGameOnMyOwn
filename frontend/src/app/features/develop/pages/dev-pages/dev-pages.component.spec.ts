import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DevPagesComponent } from './dev-pages.component';

describe('DevPagesComponent', () => {
  let component: DevPagesComponent;
  let fixture: ComponentFixture<DevPagesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DevPagesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DevPagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
