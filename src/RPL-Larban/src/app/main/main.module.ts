import { NgModule } from '@angular/core';
import { NbMenuModule } from '@nebular/theme';
import { ThemeModule } from '../@theme/theme.module';
import { ClinicsModule } from './clinics/clinics.module';
import { MainRoutingModule } from './main-routing.module';
import { MainComponent } from './main.component';

@NgModule({
  imports: [
    MainRoutingModule,
    ThemeModule,
    NbMenuModule,
    ClinicsModule,
  ],
  declarations: [
    MainComponent,
  ],
})
export class MainModule {
}
