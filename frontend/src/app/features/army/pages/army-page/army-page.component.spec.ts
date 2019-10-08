import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArmyPageComponent } from './army-page.component';

describe('ArmyPageComponent', () => {
  let component: ArmyPageComponent;
  let fixture: ComponentFixture<ArmyPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArmyPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArmyPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
