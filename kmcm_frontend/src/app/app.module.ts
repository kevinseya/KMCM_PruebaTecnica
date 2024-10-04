import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './kmcm_components/kmcm_app.Component/app.component';
import { LoginComponent } from './kmcm_components/kmcm_login/login.component';
import {FormsModule} from "@angular/forms";
import {HttpClientModule} from "@angular/common/http";
import { PersonComponent } from './kmcm_components/kmcm_person/person.component';
import { NavbarComponent } from './kmcm_components/kmcm_navbar/navbar.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    PersonComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
