import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { ClinicsComponent } from './clinics/clinics.component';
import { AppRoutingModule } from './app-routing.module';
import { CreateClinicComponent } from './clinics/create-clinic/create-clinic.component';
import { ManageClinicComponent } from './clinics/manage-clinic/manage-clinic.component';
import { ManageClinicDoctorsComponent } from './clinics/manage-clinic-doctors/manage-clinic-doctors.component';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
  declarations: [
    AppComponent,
    ClinicsComponent,
    CreateClinicComponent,
    ManageClinicComponent,
    ManageClinicDoctorsComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
