import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { SellerRequestDto, SellerRequestPageDto } from '../dtos/models';

@Injectable({
  providedIn: 'root',
})
export class SellerRequestService {
  apiName = 'Default';
  

  approveRequest = (requestId: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/seller-request/approve-request/${requestId}`,
    },
    { apiName: this.apiName,...config });
  

  createRequest = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/seller-request/request',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: SellerRequestPageDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<SellerRequestDto>>({
      method: 'GET',
      url: '/api/app/seller-request',
      params: { status: input.status, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  rejectRequest = (requestId: number, reason: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/seller-request/reject-request/${requestId}`,
      params: { reason },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
