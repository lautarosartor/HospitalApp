import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { globalRoutes } from './modules/global/global-routing.module';

@NgModule({
  imports: [RouterModule.forChild([
    ...globalRoutes,
  ])],
  exports: [RouterModule]
})
export class RutasModule { }
