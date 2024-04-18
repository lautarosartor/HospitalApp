import { Routes } from '@angular/router';
import { IndexPacienteComponent } from './components/index/index-paciente.component';

export const pacienteRoutes: Routes = [
  {
    path: 'paciente',
    component: IndexPacienteComponent,
    loadChildren: () => import('./paciente.module')
      .then(m => m.PacienteModule)
  }
];

