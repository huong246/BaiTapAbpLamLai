import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'BaiTapAbp',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44362/',
    redirectUri: baseUrl,
    clientId: 'BaiTapAbp_App',
    responseType: 'code',
    scope: 'offline_access BaiTapAbp',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44362',
      rootNamespace: 'BaiTapAbp',
    },
  },
} as Environment;
