import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@abp/ng.core';
import { AdminGuard } from './core/auth/admin.guard';
import { SellerGuard } from './core/auth/seller.guard';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'account',
    loadChildren: () => import('@abp/ng.account').then(m => m.AccountModule.forLazy()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@abp/ng.identity').then(m => m.IdentityModule.forLazy()),
  },
  {
    path: 'tenant-management',
    loadChildren: () =>
      import('@abp/ng.tenant-management').then(m => m.TenantManagementModule.forLazy()),
  },
  {
    path: 'setting-management',
    loadChildren: () =>
      import('@abp/ng.setting-management').then(m => m.SettingManagementModule.forLazy()),
  },
  {
    path: 'admin/seller-requests',
    loadChildren: () => import('./component/seller-requests/seller-requests.module').then(m=>m.SellerRequestsModule),
    canActivate: [AuthGuard, AdminGuard],
  },
  {
    path: 'my-shop',
    loadChildren: ()=> import('./component/shop-management/shop-management.module').then(m => m.ShopManagementModule),
    canActivate: [AuthGuard, SellerGuard],
  },
  {
    path: 'my-products',
    loadChildren: ()=> import('./component/product-management/product-management.module').then(m => m.ProductManagementModule),
    canActivate: [AuthGuard, SellerGuard],
  },
  {
    path: 'profile',
    loadChildren: () =>
      import('./component/customer/user-profile/user-profile.module').then(m => m.UserProfileModule),
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {})],
  exports: [RouterModule],
})
export class AppRoutingModule {}
