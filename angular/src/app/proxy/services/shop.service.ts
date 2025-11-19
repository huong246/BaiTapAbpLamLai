import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateShopDto, ShopDto, ShopPagedRequestDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  apiName = 'Default';
  

  create = (input: CreateUpdateShopDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ShopDto>({
      method: 'POST',
      url: '/api/app/shop',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/shop/${id}/remove`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ShopDto>({
      method: 'GET',
      url: `/api/app/shop/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: ShopPagedRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ShopDto>>({
      method: 'POST',
      url: '/api/app/shop/get-paged',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  update = (id: number, input: CreateUpdateShopDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ShopDto>({
      method: 'POST',
      url: `/api/app/shop/${id}/update`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
