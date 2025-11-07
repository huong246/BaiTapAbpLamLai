import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: 'admin/seller-requests',
        name: 'Duyệt yêu cầu mở bán',
        iconClass: 'fas fa-user-check',
        order: 2,
        layout: eLayoutType.application,
        requiredPolicy: 'MyProject.SellerRequests.Default',
      },
      {
        path: '/my-shop',
        name: 'Cửa hàng',
        iconClass: 'fas fa-store',
        order: 2,
        layout: eLayoutType.application,
        requiredPolicy: 'MyProject.Shops.Default',
      },
      {
        path: '/my-products',
        name: 'Sản phẩm',
        iconClass: 'fas fa-box',
        order:3,
        layout: eLayoutType.application,
        requiredPolicy: 'Products.Default',
      },
    ]);
  };
}
