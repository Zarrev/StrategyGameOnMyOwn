import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AttackPageComponent } from './attack-page.component';

describe('AttackPageComponent', () => {
  let component: AttackPageComponent;
  let fixture: ComponentFixture<AttackPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AttackPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AttackPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
