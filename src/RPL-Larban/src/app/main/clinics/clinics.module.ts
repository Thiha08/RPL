import { NgModule } from '@angular/core';
import { ClinicsComponent } from './clinics.component';
import { CreateClinicComponent } from './create-clinic/create-clinic.component';
import { ManageClinicComponent } from './manage-clinic/manage-clinic.component';


@NgModule({
  imports: [

  ],
  declarations: [
    ClinicsComponent,
    CreateClinicComponent,
    ManageClinicComponent,
  ],
})
export class ClinicsModule { }
