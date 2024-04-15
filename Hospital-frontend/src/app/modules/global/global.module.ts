import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

//import { GlobalRoutingModule } from './global-routing.module';
import { IndexComponent } from './components/index/index.component';
import { MenuGlobalComponent } from './components/menu-global/menu-global.component';

import {MatToolbarModule} from '@angular/material/toolbar';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';

@NgModule({
  declarations: [
    IndexComponent,
    MenuGlobalComponent
  ],
  imports: [
    CommonModule,
    //GlobalRoutingModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
  ],
  exports: [
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
  ]
})
export class GlobalModule { }
