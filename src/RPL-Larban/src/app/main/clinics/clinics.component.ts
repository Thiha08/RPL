import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '../../../shared/common/app-component-base';

@Component({
  selector: 'rpl-clinics',
  templateUrl: './clinics.component.html',
  styleUrls: ['./clinics.component.scss'],
})
export class ClinicsComponent extends AppComponentBase implements OnInit {

  constructor(
    injector: Injector,
  ) {
    super(injector);
  }

  ngOnInit(): void {
  }

}
