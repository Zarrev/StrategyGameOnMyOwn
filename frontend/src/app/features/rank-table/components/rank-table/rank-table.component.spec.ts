import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RankTableComponent } from './rank-table.component';

describe('RankTableComponent', () => {
  let component: RankTableComponent;
  let fixture: ComponentFixture<RankTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RankTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RankTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
