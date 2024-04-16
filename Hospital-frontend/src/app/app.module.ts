import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { GlobalModule } from './modules/global/global.module';
import { FormComponent } from './modules/egreso/components/form/form.component';
import { IndexComponent } from './modules/egreso/components/index/index.component';

@NgModule({
  declarations: [
    AppComponent,
    FormComponent,
    IndexComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    GlobalModule,
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
