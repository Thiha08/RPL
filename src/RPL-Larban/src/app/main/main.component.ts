import { Component } from '@angular/core';
import { MAIN_MENU_ITEMS } from './main-menu';

@Component({
  selector: 'rpl-main',
  styleUrls: ['pages.component.scss'],
  template: `
    <ngx-one-column-layout>
      <nb-menu [items]="menu"></nb-menu>
      <router-outlet></router-outlet>
    </ngx-one-column-layout>
  `,
})
export class MainComponent {

  menu = MAIN_MENU_ITEMS;
}
