import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VoditregistrComponent } from './voditregistr.component';

describe('VoditregistrComponent', () => {
  let component: VoditregistrComponent;
  let fixture: ComponentFixture<VoditregistrComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VoditregistrComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VoditregistrComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
