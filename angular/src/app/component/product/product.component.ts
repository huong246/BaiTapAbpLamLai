import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { NzButtonComponent } from 'ng-zorro-antd/button';
import { PagedResultDto } from '@abp/ng.core';
import { ProductDto } from '@proxy/dtos';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ProductService } from '@proxy/services';
import { Router } from '@angular/router';
import { finalize, Subject } from 'rxjs';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ProductDetailComponent } from './product-detail/product-detail.component';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [
    NzButtonComponent
  ],
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss'
})
export class ProductComponent implements OnInit, OnDestroy{
  product: PagedResultDto<ProductDto> = {
    items: [],
    totalCount: 0
  } as PagedResultDto<ProductDto>
  private destroy$ = new Subject<void>();

  searchForm: FormGroup;
  pageIndex =1;
  maxResultCount =10;
  loading: boolean = false;
  constructor(private productService: ProductService, private router : Router,
              private fb: FormBuilder, private modal: NzModalService) {
    this.searchForm = this.fb.group({
      filter: [' '],
      type: [null]
    });
  }

  ngOnInit() {
    this.loadProduct();

  }
  loadProduct() {
    this.loading = true;
    //bỏ qua skipCount phần tử
    const skipCount = (this.pageIndex -1)*this.maxResultCount;

    const params = {
      filter: this.searchForm.get('filter')?.value,
      skipCount: skipCount,
      maxResultCount: this.maxResultCount,
    };
    this.productService.getList(params)
      .pipe(finalize(() => this.loading = false))
      .subscribe(response =>
      {
        this.product = response;
      });
  }
  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  onPageChange(page: number) {
    this.pageIndex = page;
    this.loadProduct();
  }
  onPageSizeChange(size: number) {
    this.maxResultCount = size;
    this.pageIndex =1;
    this.loadProduct();
  }
  resetSearch() {
    this.searchForm.reset(
      {
        filter: '',
      }
    );
    this.pageIndex =1;
    this.loadProduct();
  }

  search() {
    this.pageIndex = 1;
    this.loadProduct();
  }

  onDelete(product : ProductDto) {
    this.modal.confirm({
      nzTitle: 'Confirm Delete?',
      nzContent: 'Are you sure you want to delete this product?',
      nzOkText: 'Delete',
      nzOkType: 'primary',
      nzOkDanger: true,
      nzCancelText: 'Cancel',
      nzOnOk: () => this.executeDelete(product.id)
    });
  }

  executeDelete(id: number) {
    this.loading = true;
    this.productService.delete(id).pipe(finalize(() => this.loading = false)).subscribe(
      {
        next: ()=>
        {
          this.loadProduct();
        },
        error: (err) =>
        {
          console.error(err);
        }
      }
    );
  }

  onModalSave()
  {
    this.loadProduct();
  }
  trackById(index: number, item: any): number
  {
    return item.id;
  }

  showProductDetail(productId: number)
  {
    this.modal.create({
      nzTitle: 'Detail of Product',
      nzContent: ProductDetailComponent,
      nzData: {
        productId: productId,
      },
      nzClassName: 'product-detail',
      nzFooter: null
    });
  }
}
