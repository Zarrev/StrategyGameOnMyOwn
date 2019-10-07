import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BuildingPagesComponent } from './building-pages.component';

describe('BuildingPagesComponent', () => {
  let component: BuildingPagesComponent;
  let fixture: ComponentFixture<BuildingPagesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BuildingPagesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BuildingPagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
