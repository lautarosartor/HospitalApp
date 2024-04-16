import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MenuGlobalComponent } from './modules/global/components/menu-global/menu-global.component';

const routes: Routes = [
  {
    path: '',
    component: MenuGlobalComponent,
    loadChildren: () => import('./app.rutas')
      .then(m => m.RutasModule)
  },
  {
    path: '**',
    redirectTo: '/'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
