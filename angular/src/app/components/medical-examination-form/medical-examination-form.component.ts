import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import { MedicalVoiceSignalRService } from '@proxy/services/medical-voice-signalr.service';
import { FIELD_MAPPING, MedicalExaminationForm } from '@proxy/models/medical-form';
import { NgClass } from '@angular/common';
import { BaseCoreModule } from '@abp/ng.core';

@Component({
  selector: 'app-medical-examination-form',
  standalone: true,
  imports: [
    NgClass,
    BaseCoreModule
  ],
  templateUrl: './medicalExaminationForm.html',
  styleUrl: './medicalExaminationForm.scss'
})
export class MedicalExaminationFormComponent implements OnInit, OnDestroy{
  medicalForm!: FormGroup;
  connectionStatus: string = 'disconnected';
  processingStatus: string = '';
  updatedFields: Set<string> = new Set();

  private subscriptions: Subscription[] = [];

  constructor(
    private fb: FormBuilder,
    private signalRService: MedicalVoiceSignalRService
  ) {}

  ngOnInit() {
    this.initForm();
    this.connectToSignalR();
    this.subscribeToUpdates();
  }

  ngOnDestroy() {
    this.subscriptions.forEach(sub => sub.unsubscribe());
    this.signalRService.stopConnection();
  }

  private initForm(): void {
    this.medicalForm = this.fb.group({
      patientName: [''],
      gender: [''],
      birthYear: [],
      age: [''],

      // Sinh hiệu
      pulse: [''],
      bloodPressure: [''],
      breathingRate: [''],
      temperature: [''],
      height: [''],
      weight: [''],
      bmi: [''],
      waistline: [''],

      // Tiền sử
      personalHistory: [''],
      familyHistory: [''],
      allergyHistory: [''],
      medicalHistory: [''],

      // Khám lâm sàng
      bodyExamination: [''],
      partExamination: [''],
      otherIssues: [''],
      leftEyeDegree: [''],
      rightEyeDegree: [''],
      fetalHeart: [''],

      // ICD
      icdMain: [''],
      icdSub: [''],

      // Chẩn đoán & Điều trị
      reasonForVisit: [''],
      initialDiagnosis: [''],
      finalDiagnosis: [''],
      diseaseProgress: [''],
      treatment: [''],
      resultType: [''],
      visitType: [''],

      // Hẹn khám
      revisitAfterDays: [''],
      revisitDate: [''],
      revisitTime: [''],
      revisitNote: [''],

      // Khác
      note: [''],
      doctorNote: ['']
    });
  }

  private async connectToSignalR(): Promise<void> {
    const statusSub = this.signalRService.connectionStatus$.subscribe(status => {

      this.connectionStatus = status;
      console.log('🔔 Connection status updated:', status);

    });
    this.subscriptions.push(statusSub);

    try {
      // 2. Bây giờ mới gọi lệnh kết nối
      await this.signalRService.startConnection();
    }
    catch (error) {
      console.error('Failed',error);
      this.connectionStatus = error;
    }
  }

  private subscribeToUpdates(): void {
    const fieldsSub = this.signalRService.medicalFields$.subscribe(
      data => {
        console.log('📥 Received fields:', data.fields);
        this.autoFillForm(data.fields);
        this.showNotification(`Đã nhận ${data.fields.length} trường từ giọng nói`);

      }
    );
    this.subscriptions.push(fieldsSub);
    const statusSub = this.signalRService.statusUpdate$.subscribe(
      status => {
        this.processingStatus = status.message || status.status;
        console.log('📊 Status:', status.status, status.message);

        if (status.status === 'completed') {
          setTimeout(() => {
            this.processingStatus = '';
          }, 3000);
        }
      }
    );
    this.subscriptions.push(statusSub);
  }
  private autoFillForm(fields: Array<{ field_name: string; content: string }>): void {
    this.updatedFields.clear();

    fields.forEach(field => {
      this.updateSingleField(field.field_name, field.content);
    });
    setTimeout(() => {
      this.updatedFields.clear();
    }, 2000);
  }

  private updateSingleField(fieldName: string, content: string): void {
    // Map từ field_name (API) sang form control name
    const controlName = FIELD_MAPPING[fieldName];

    if (controlName && this.medicalForm.controls[controlName]) {
      // Set value
      this.medicalForm.controls[controlName].setValue(content);

      // Mark as touched
      this.medicalForm.controls[controlName].markAsTouched();

      // Thêm vào danh sách updated (để highlight)
      this.updatedFields.add(controlName);

      console.log(`✅ Updated ${controlName} = ${content}`);
    } else {
      console.warn(`⚠️ No mapping for field: ${fieldName}`);
    }
  }

  /**
   * Kiểm tra field có được update không (để áp dụng CSS animation)
   */
  isFieldUpdated(controlName: string): boolean {
    return this.updatedFields.has(controlName);
  }

  /**
   * Submit form
   */
  onSubmit(): void {
    if (this.medicalForm.valid) {
      const formData: MedicalExaminationForm = this.medicalForm.value;
      console.log('💾 Saving form:', formData);

      // TODO: Gọi API save form
      // this.medicalService.saveExamination(formData).subscribe(...)

      this.showNotification('Lưu thông tin thành công');
    } else {
      this.showNotification('Vui lòng điền đầy đủ thông tin bắt buộc', 'error');
    }
  }

  /**
   * Reset form
   */
  resetForm(): void {
    this.medicalForm.reset({
      gender: 'Nam',
      birthYear: 2001
    });
    this.updatedFields.clear();
  }

  /**
   * Hiển thị notification (tích hợp với snackbar hoặc toast)
   */
  private showNotification(message: string, type: 'success' | 'error' | 'info' = 'success'): void {
    // TODO: Implement notification service
    console.log(`📢 [${type.toUpperCase()}] ${message}`);
  }

  /**
   * Test connection button
   */
  testConnection(): void {
    console.log('📡 Connection ID:', this.signalRService.getConnectionId());
    console.log('📡 Is Connected:', this.signalRService.isConnected());
    console.log('📡 Status:', this.connectionStatus);
  }
}

