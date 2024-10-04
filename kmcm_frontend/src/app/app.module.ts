import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './kmcm_components/kmcm_app.Component/app.component';
import { Kmmc_LoginComponent } from './kmcm_components/kmcm_login/kmmc_login.component';
import {FormsModule} from "@angular/forms";
import {HttpClientModule} from "@angular/common/http";
import { Kmmc_PersonComponent } from './kmcm_components/kmcm_person/kmmc_person.component';
import { Kmmc_NavbarComponent } from './kmcm_components/kmcm_navbar/kmmc_navbar.component';
import { Kmmc_MenuComponent } from './kmcm_components/kmmc_menu/kmmc_-menu.component';
import { Kmmc_UserComponent } from './kmcm_components/kmmc_user/kmmc_user.component';

@NgModule({
  declarations: [
    AppComponent,
    Kmmc_LoginComponent,
    Kmmc_PersonComponent,
    Kmmc_NavbarComponent,
    Kmmc_MenuComponent,
    Kmmc_UserComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
