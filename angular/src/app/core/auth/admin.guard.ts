import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { ConfigStateService } from '@abp/ng.core';

//danh dau co the inject cac service khac
@Injectable({providedIn: 'root'})
export class AdminGuard implements CanActivate {
  // inject service vao trong nay
  constructor(private router: Router, private configState: ConfigStateService) {
  }
  canActivate(): boolean
  {
    const currentUser = this.configState.getOne('currentUser');
    const isAdmin = currentUser?.roles?.includes('admin');
    if (isAdmin) {
      return true;
    }
    this.router.navigate(['/']);
    return false;
  }
}
