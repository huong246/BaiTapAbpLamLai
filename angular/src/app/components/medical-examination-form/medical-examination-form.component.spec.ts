import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicalExaminationFormComponent } from './medical-examination-form.component';

describe('MedicalExaminationFormComponent', () => {
  let component: MedicalExaminationFormComponent;
  let fixture: ComponentFixture<MedicalExaminationFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MedicalExaminationFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MedicalExaminationFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
