import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CartDto, CartPagedRequestDto, CreateUpdateCartDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  apiName = 'Default';
  

  create = (input: CreateUpdateCartDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CartDto>({
      method: 'POST',
      url: '/api/app/cart/or-update',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/cart/${id}/delete`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CartDto>({
      method: 'GET',
      url: `/api/app/cart/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: CartPagedRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CartDto>>({
      method: 'GET',
      url: '/api/app/cart',
      params: { filter: input.filter, filterFts: input.filterFts, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: number, input: CreateUpdateCartDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CartDto>({
      method: 'POST',
      url: `/api/app/cart/${id}/update`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
