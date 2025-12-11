
export interface MedicalExaminationForm {

  patientName?: string;
  gender?: string;
  birthYear?: number;
  age?: number;
  height?: string;
  weight?: string;
  bmi?: string;
  waistline?: string;


  pulse?: string;
  bloodPressure?: string;
  breathingRate?: string;
  temperature?: string;

  personalHistory?: string;
  familyHistory?: string;
  allergyHistory?: string;
  medicalHistory?: string;

  bodyExamination?: string;
  partExamination?: string;
  otherIssues?: string;
  leftEyeDegree?: string;
  rightEyeDegree?: string;
  fetalHeart?: string;

  icdMain?: string;
  icdSub?: string;
  icdList?: IcdItem[];

  reasonForVisit?: string;
  initialDiagnosis?: string;
  finalDiagnosis?: string;
  diseaseProgress?: string;
  treatment?: string;
  resultType?: string;
  visitType?: string;

  revisitAfterDays?: string;
  revisitDate?: string;
  revisitTime?: string;
  revisitNote?: string;

  note?: string;
  doctorNote?: string;
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
