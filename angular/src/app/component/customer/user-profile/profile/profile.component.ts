import { Component, OnInit } from '@angular/core';
import { ConfigStateService } from '@abp/ng.core';
import { SellerRequestService } from '@proxy/services';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit{
  hasSellerRole = false;
  hasPendingRequest = false;
  constructor(private configState : ConfigStateService, private sellerRequestService : SellerRequestService) {
  }

  ngOnInit() {
    const currentUser = this.configState.getOne('currentUser');
    this.hasSellerRole = currentUser?.roles?.includes("Seller");
  }
}
