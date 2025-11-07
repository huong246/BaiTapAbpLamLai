import { CanActivate,   Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { ConfigStateService } from '@abp/ng.core';

@Injectable({providedIn: 'root'})
export class SellerGuard implements CanActivate {
  constructor(private configState : ConfigStateService, private router: Router) {
  }
  canActivate(): boolean
  {
    const currentUser = this.configState.getOne('currentUser');
    const isSeller = currentUser.roles.includes('Seller');
    if(isSeller)
    {
      return true;
    }
    this.router.navigate(['/']);
    return false;
  }
}
