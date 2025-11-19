import { Component, Output, EventEmitter} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '@proxy/services';
import {NzMessageService} from 'ng-zorro-antd/message';

@Component({
  selector: 'app-create-or-update',
  standalone: true,
  imports: [],
  templateUrl: './create-or-update.component.html',
  styleUrl: './create-or-update.component.scss'
})
class CreateOrUpdateProductComponent {
  @Output() onSaved = new EventEmitter<void>();
  visible = false;
  provinceId?: number;
  visibiChange = new EventEmitter<boolean>();

  form: FormGroup;
  isEditMode = false;
  loading = false;
  submitting = false;
  isCodeEditTable =false;

  constructor(private fb: FormBuilder, private productService: ProductService, private message: NzMessageService) {
    this.form = this.fb.group({
      code: ['', [Validators.required, Validators.minLength(50)]],
      name: ['', [Validators.required, Validators.minLength(100)]],
    })
  }

}


