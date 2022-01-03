import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'rpl-create-clinic',
  templateUrl: './create-clinic.component.html',
  styleUrls: ['./create-clinic.component.scss'],
})
export class CreateClinicComponent extends AppComponentBase implements OnInit {

  constructor(
    injector: Injector,
  ) {
    super(injector);
  }

  ngOnInit(): void {
  }

}
