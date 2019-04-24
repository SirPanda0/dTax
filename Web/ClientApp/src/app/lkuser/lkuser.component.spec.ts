import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LkuserComponent } from './lkuser.component';

describe('LkuserComponent', () => {
  let component: LkuserComponent;
  let fixture: ComponentFixture<LkuserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LkuserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LkuserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
