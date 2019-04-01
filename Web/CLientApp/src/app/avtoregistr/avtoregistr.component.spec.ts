import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AvtoregistrComponent } from './avtoregistr.component';

describe('AvtoregistrComponent', () => {
  let component: AvtoregistrComponent;
  let fixture: ComponentFixture<AvtoregistrComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AvtoregistrComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AvtoregistrComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
