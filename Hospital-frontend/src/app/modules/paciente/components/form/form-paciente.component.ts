import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HttpService } from '../../../../services/http.service';

@Component({
  selector: 'app-form-paciente',
  templateUrl: './form-paciente.component.html',
  styleUrl: './form-paciente.component.css'
})
export class FormPacienteComponent implements OnInit {
  title: string = 'Crear';
  entidad = 'Paciente';

  formPaciente!: FormGroup;
  modoEdicion: boolean = false;
  modoDetalle: boolean = false;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<FormPacienteComponent>,
    private fb: FormBuilder,
    private httpService: HttpService,
  ) { }

  ngOnInit(): void {

    if (this.data.id) {
      this.title = 'Detalle';
      this.modoDetalle = true;
      this.cargarDatosParaDetalles();
    }

    this.initForm();
  }

  initForm() {
    this.formPaciente = this.fb.group({
      dni: [{ value: null, disabled: this.modoDetalle }, [Validators.required]],
      nombre: [{ value: null, disabled: this.modoDetalle }, [Validators.required]],
      apellidoPaterno: [{ value: null, disabled: this.modoDetalle }, [Validators.required]],
      apellidoMaterno: [{ value: null, disabled: this.modoDetalle }],
      direccion: [{ value: null, disabled: this.modoDetalle }, [Validators.required]],
      celular: [{ value: null, disabled: this.modoDetalle }, [Validators.required]],
      email: [{ value: null, disabled: this.modoDetalle }, [Validators.required, Validators.email]],
    });
  }

  GuardarPaciente() {
    const dataPaciente = this.formPaciente.value as any;
    if (this.modoEdicion) {
      this.httpService.Actualizar(this.data.id, dataPaciente, this.entidad)
        .subscribe({
          next: () => {
            this.dialogRef.close('editar');
          },
          error: (error) => {
            console.log('No se pudo modificar el registro. ', error);
          }
        });
    }
    else {
      const { dni, nombre, apellidoPaterno, apellidoMaterno, direccion, celular, email } = this.formPaciente.value;

      const nuevoPaciente: any = {
        dni: dni,
        nombre: nombre,
        apellidoPaterno: apellidoPaterno,
        apellidoMaterno: apellidoMaterno,
        direccion: direccion,
        celular: celular,
        email: email
      }

      this.httpService.Crear(nuevoPaciente, this.entidad)
        .subscribe({
          next: () => {
            this.dialogRef.close('guardar');
          },
          error: (error) => {
            console.log('Error al crear registro. ', error);
          }
        });
    }
  }

  Editar() {
    this.title = 'Editar';
    this.modoEdicion = true;
    this.modoDetalle = false;
    this.initForm();
    this.cargarDatosParaDetalles();
    this.data.tipo = 'EDITAR';
  }

  Cancelar() {
    this.dialogRef.close();
  }

  cargarDatosParaDetalles() {
    this.httpService.LeerUno(this.data.id, this.entidad)
      .subscribe({
        next: (respuesta: any) => {
          this.formPaciente.patchValue({
            dni: respuesta.Datos.Dni,
            nombre: respuesta.Datos.Nombre,
            apellidoPaterno: respuesta.Datos.ApellidoPaterno,
            apellidoMaterno: respuesta.Datos.ApellidoMaterno,
            direccion: respuesta.Datos.Direccion,
            celular: respuesta.Datos.Celular,
            email: respuesta.Datos.Email,
          });
        },
        error: (error) => {
          console.log('Error al obtener detalles del paciente. ', error);
        }
      });
  }
}
