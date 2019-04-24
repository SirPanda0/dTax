import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LkdriverComponent } from './lkdriver.component';

describe('LkdriverComponent', () => {
  let component: LkdriverComponent;
  let fixture: ComponentFixture<LkdriverComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LkdriverComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LkdriverComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
