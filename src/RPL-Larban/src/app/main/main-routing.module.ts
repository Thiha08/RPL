import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClinicsComponent } from './clinics/clinics.component';
import { ManageClinicComponent } from './clinics/manage-clinic/manage-clinic.component';
import { MainComponent } from './main.component';




export const routes: Routes = [{
  path: '',
  component: MainComponent,
  children: [
    {
      path: 'clinics',
      component: ClinicsComponent,
    },
    {
      path: 'clinics/:id',
      component: ManageClinicComponent,
    },
  ],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MainRoutingModule {
}





