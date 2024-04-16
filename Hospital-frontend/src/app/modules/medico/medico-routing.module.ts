import { Routes } from '@angular/router';
import { IndexComponent } from './components/index/index.component';

export const medicoRoutes: Routes = [
  {
    path: 'medico',
    component: IndexComponent,
    loadChildren: () => import('./medico.module')
      .then(m => m.MedicoModule)
  }
];
