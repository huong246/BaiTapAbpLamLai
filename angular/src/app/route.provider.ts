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
        path: '/shop',
        name: 'Menu:Shops',
        iconClass: 'fa-solid fa-shop',
        order: 2,
        layout: eLayoutType.application,
        requiredPolicy: 'RolePermissions.Shops.Default',
      },
      {
        path: '/category',
        name: 'Menu:Categories',
        iconClass: '<fa-solid fa-list',
        parentName: 'Menu:Shops',
        layout: eLayoutType.application,
      },
      {
        path: '/product',
        name: 'Menu:Products',
        iconClass: 'fas fa-box',
        parentName: 'Menu:Categories',
        layout: eLayoutType.application,
        requiredPolicy: 'RolePermissions.Products.Default',
      }
    ]);
  };
}
