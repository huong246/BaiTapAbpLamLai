import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { NzContentComponent, NzHeaderComponent, NzLayoutComponent, NzSiderComponent } from 'ng-zorro-antd/layout';
import { NzBreadCrumbComponent } from 'ng-zorro-antd/breadcrumb';

@NgModule({
  declarations: [HomeComponent],
  imports: [SharedModule, HomeRoutingModule, NzLayoutComponent, NzHeaderComponent, NzSiderComponent, NzContentComponent, NzBreadCrumbComponent]
})
export class HomeModule {}
