import type { AuditedEntityDto, EntityDto, PagedAndSortedResultRequestDto, PagedResultRequestDto } from '@abp/ng.core';
import type { RequestStatus } from '../entities/enum/request-status.enum';

export interface BasePagedResultRequestDto extends PagedResultRequestDto {
  filter?: string;
  filterFts?: string;
}

export interface CartDto extends AuditedEntityDto<number> {
  productId: number;
  shopId: number;
  customerId: number;
  quantity: number;
}

export interface CartPagedRequestDto extends BasePagedResultRequestDto {
}

export interface CategoryDto extends AuditedEntityDto<number> {
  name?: string;
}

export interface CategoryPagedRequestDto extends BasePagedResultRequestDto {
}

export interface CreateUpdateCartDto {
  productId: number;
  shopId: number;
  customerId: number;
  quantity: number;
}

export interface CreateUpdateCategoryDto {
  name?: string;
}

export interface CreateUpdateProductDto {
  name: string;
  price: number;
  stock: number;
  shopId: number;
  categoryId: number;
}

export interface CreateUpdateShopDto {
  name?: string;
  address?: string;
}

export interface ProductDto extends AuditedEntityDto<number> {
  name?: string;
  price: number;
  stock: number;
  shopId: number;
  profilePictureUrl?: string;
}

export interface ProductPagedRequestDto extends BasePagedResultRequestDto {
}

export interface SellerRequestDto extends EntityDto<number> {
  userId?: string;
  reviewerId?: string;
  status: RequestStatus;
  creationAt?: string;
  reviewAt?: string;
  reason?: string;
}

export interface SellerRequestPageDto extends PagedAndSortedResultRequestDto {
  status?: RequestStatus;
}

export interface ShopDto extends AuditedEntityDto<number> {
  name?: string;
  address?: string;
}

export interface ShopPagedRequestDto extends BasePagedResultRequestDto {
}
