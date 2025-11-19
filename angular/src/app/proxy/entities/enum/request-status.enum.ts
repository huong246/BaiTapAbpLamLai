import { mapEnumToOptions } from '@abp/ng.core';

export enum RequestStatus {
  Pending = 0,
  Approved = 1,
  Rejected = 2,
}

export const requestStatusOptions = mapEnumToOptions(RequestStatus);
