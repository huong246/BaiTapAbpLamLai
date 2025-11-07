import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateProductDto, ProductDto, ProductPagedRequestDto } from '../dtos/models';
import type { IFormFile } from '../microsoft/asp-net-core/http/models';
import type { IActionResult } from '../microsoft/asp-net-core/mvc/models';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  apiName = 'Default';
  

  create = (input: CreateUpdateProductDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProductDto>({
      method: 'POST',
      url: '/api/app/product',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/product/${id}/delete`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProductDto>({
      method: 'GET',
      url: `/api/app/product/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: ProductPagedRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ProductDto>>({
      method: 'POST',
      url: '/api/app/product/get-paged',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  getPicture = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, IActionResult>({
      method: 'POST',
      url: `/api/app/product/${id}/get-picture`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: number, input: CreateUpdateProductDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProductDto>({
      method: 'POST',
      url: `/api/app/product/${id}/update`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  uploadPicture = (id: number, file: IFormFile, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/product/${id}/upload-picture`,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
