import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GlobalModule } from '../global/global.module';
import { IndexPacienteComponent } from './components/index/index-paciente.component';
import { FormPacienteComponent } from './components/form/form-paciente.component';

@NgModule({
  declarations: [
    IndexPacienteComponent,
    FormPacienteComponent,
  ],
  imports: [
    CommonModule,
    GlobalModule,
  ]
})
export class PacienteModule { }
