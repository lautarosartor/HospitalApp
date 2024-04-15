import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './components/index/index.component';

export const globalRoutes: Routes = [
  {
    path: '',
    component: IndexComponent,
    loadChildren: () => import('./global.module')
      .then(m => m.GlobalModule)
  }
];

/* @NgModule({
  imports: [RouterModule.forChild(globalRoutes)],
  exports: [RouterModule]
})
export class GlobalRoutingModule { } */
