// src/app/models/medical-form.interface.ts

export interface MedicalExaminationForm {
  // ===== Thông tin bệnh nhân =====
  patientName?: string;
  gender?: string;
  birthYear?: number;
  age?: number;
  height?: string;          // Chiều cao
  weight?: string;          // Cân nặng
  bmi?: string;
  waistline?: string;       // Vòng bụng

  // ===== Sinh hiệu =====
  pulse?: string;           // Mạch (mach)
  bloodPressure?: string;   // Huyết áp (huyet_ap)
  breathingRate?: string;   // Nhịp thở (nhip_tho)
  temperature?: string;     // Nhiệt độ (nhiet_do)

  // ===== Tiền sử =====
  personalHistory?: string; // Tiền sử bản thân (tien_su_ban_than)
  familyHistory?: string;   // Tiền sử gia đình (tien_su_gia_dinh)
  allergyHistory?: string;  // Tiền sử dị ứng (tien_su_di_ung)
  medicalHistory?: string;  // Bệnh sử (benh_su)

  // ===== Khám lâm sàng =====
  bodyExamination?: string; // Toàn thân (toan_than)
  partExamination?: string; // Bộ phận (bo_phan)
  otherIssues?: string;     // Vấn đề khác (van_de_khac)
  leftEyeDegree?: string;   // Độ cận MT (do_can_mt)
  rightEyeDegree?: string;  // Độ cận MP (do_can_mp)
  fetalHeart?: string;      // Tim thai (tim_thai)

  // ===== ICD =====
  icdMain?: string;         // Mã bệnh chính ICD-10
  icdSub?: string;          // Mã bệnh phụ ICD-10
  icdList?: IcdItem[];      // Danh sách ICD

  // ===== Chẩn đoán & Điều trị =====
  reasonForVisit?: string;  // Lý do khám (ly_do_kham)
  initialDiagnosis?: string;// Chẩn đoán ban đầu (chan_doan_ban_dau)
  finalDiagnosis?: string;  // Chẩn đoán
  diseaseProgress?: string; // Diễn biến bệnh
  treatment?: string;       // Phương pháp điều trị
  resultType?: string;      // Kết quả khám
  visitType?: string;       // Loại hình khám

  // ===== Hẹn khám =====
  revisitAfterDays?: string;// Khám lại sau (số ngày)
  revisitDate?: string;     // Ngày khám lại
  revisitTime?: string;     // Giờ khám lại
  revisitNote?: string;     // Nội dung hẹn khám

  // ===== Khác =====
  note?: string;            // Ghi chú
  doctorNote?: string;      // Lời dặn
}

export interface IcdItem {
  code: string;
  name: string;
}


export const FIELD_MAPPING: { [key: string]: keyof MedicalExaminationForm } = {
  'mach': 'pulse',
  'huyet_ap': 'bloodPressure',
  'nhip_tho': 'breathingRate',
  'nhiet_do': 'temperature',
  'chieu_cao': 'height',
  'can_nang': 'weight',
  'bmi': 'bmi',
  'vong_bung': 'waistline',

  'tien_su_ban_than': 'personalHistory',
  'tien_su_gia_dinh': 'familyHistory',
  'tien_su_di_ung': 'allergyHistory',
  'benh_su': 'medicalHistory',

  'toan_than': 'bodyExamination',
  'bo_phan': 'partExamination',
  'van_de_khac': 'otherIssues',
  'do_can_mt': 'leftEyeDegree',
  'do_can_mp': 'rightEyeDegree',
  'tim_thai': 'fetalHeart',

  'ly_do_kham': 'reasonForVisit',
  'chan_doan_ban_dau': 'initialDiagnosis',
  'chan_doan': 'finalDiagnosis',
  'dien_bien_benh': 'diseaseProgress',
  'phuong_phap_dieu_tri': 'treatment',
  'ket_qua_kham': 'resultType',
  'loai_hinh_kham': 'visitType',

  'kham_lai_sau': 'revisitAfterDays',
  'ngay_kham_lai': 'revisitDate',
  'gio_kham_lai': 'revisitTime',
  'noi_dung_hen_kham': 'revisitNote',

  'ghi_chu': 'note',
  'loi_dan': 'doctorNote',
};
