import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HttpService } from '../../../../services/http.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrl: './form.component.css'
})
export class FormComponent implements OnInit {

  title: string = 'Crear';
  entidad = 'Medico';

  formMedico!: FormGroup;
  modoEdicion: boolean = false; //Indicar si estamos en edicion
  modoDetalle: boolean = false; //Indicar si estamos en detalle

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<FormComponent>,
    private fb: FormBuilder,
    private httpService: HttpService,
  ) {}

  ngOnInit(): void {
    //Vemos en que instancia estamos (CREAR/DETALLE/MODIFICAR)
    //console.log(this.data.tipo);

    if (this.data.id) {
      //console.log('ID: ' + this.data.id);
      this.title = 'Detalle';
      this.modoDetalle = true;
      this.cargarDatosParaDetalles();
    }

    this.initForm();
  }

  initForm() {
    this.formMedico = this.fb.group({
      dni: [{ value: null, disabled: this.modoDetalle }, [Validators.required]],
      nombre: [{ value: null, disabled: this.modoDetalle }, [Validators.required]],
      apellidoPaterno: [{ value: null, disabled: this.modoDetalle }, [Validators.required]],
      apellidoMaterno: [{ value: null, disabled: this.modoDetalle }],
      celular: [{ value: null, disabled: this.modoDetalle}, [Validators.required]],
      esEspecialista: [{ value: true, disabled: this.modoDetalle }, [Validators.required]],
      habilitado: [{ value: true, disabled: this.modoDetalle }, [Validators.required]],
    });
  }

  Guardar() {
    const dataMedico = this.formMedico.value as any;
    if (this.modoEdicion) {
      this.httpService.Actualizar(this.data.id, dataMedico, this.entidad)
        .subscribe({
          next: () => {
            this.dialogRef.close('editar');
          },
          error: (error) => {
            console.log("No se pudo modificar el registro. ", error);
          }
        });
    }
    else {
      const { dni, nombre, apellidoPaterno, apellidoMaterno, celular, esEspecialista, habilitado } = this.formMedico.value;

      const nuevoMedico: any = {
        dni: dni,
        nombre: nombre,
        apellidoPaterno: apellidoPaterno,
        apellidoMaterno: apellidoMaterno,
        celular: celular,
        esEspecialista: esEspecialista,
        habilitado: habilitado
      }

      this.httpService.Crear(nuevoMedico, this.entidad)
        .subscribe({
          next: () => {
            //console.log('Registro agregado exitosamente');
            //Cierra el dialog y envia 'guardar' como resultado
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
    this.modoEdicion = true;        //Activamos edicion
    this.modoDetalle = false;       //Habilitamos las casillas y el boton guardar
    this.initForm();                //Iniciamos el form
    this.cargarDatosParaDetalles(); //Llenamos con los datos
    this.data.tipo = 'EDITAR';      //console.log(this.data);

    //Otra manera de habilitar las casillas
    /* this.formMedico.controls['dni'].enable();
    this.formMedico.controls['nombre'].enable();
    this.formMedico.controls['apellidoPaterno'].enable();
    this.formMedico.controls['apellidoMaterno'].enable();
    this.formMedico.controls['celular'].enable();
    this.formMedico.controls['esEspecialista'].enable();
    this.formMedico.controls['habilitado'].enable(); */
  }

  Cancelar() {
    this.dialogRef.close();
  }

  cargarDatosParaDetalles() {
    this.httpService.LeerUno(this.data.id, this.entidad)
      .subscribe({
        next: (respuesta: any) => {
          //this.formMedico.patchValue = respuesta.Datos;
          //console.log(this.formMedico.patchValue);
          //console.log(respuesta);
          // Se utiliza la función patchValue para asignar los valores
          this.formMedico.patchValue({
            dni: respuesta.Datos.Dni,
            nombre: respuesta.Datos.Nombre,
            apellidoPaterno: respuesta.Datos.ApellidoPaterno,
            apellidoMaterno: respuesta.Datos.ApellidoMaterno,
            celular: respuesta.Datos.Celular,
            esEspecialista: respuesta.Datos.EsEspecialista,
            habilitado: respuesta.Datos.Habilitado,
          });
        },
        error: (error) => {
          console.log('Error al obtener detalles del medico. ', error);
        }
      });
  }
}
