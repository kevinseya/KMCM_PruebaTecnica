import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {Kmmc_LoginComponent} from "./kmcm_components/kmcm_login/kmmc_login.component";
import {Kmmc_PersonComponent} from "./kmcm_components/kmcm_person/kmmc_person.component";
import {Kmmc_MenuComponent} from "./kmcm_components/kmmc_menu/kmmc_-menu.component";
import {Kmmc_UserComponent} from "./kmcm_components/kmmc_user/kmmc_user.component";

const routes: Routes = [
  { path: 'login', component: Kmmc_LoginComponent },
  { path: 'persons', component: Kmmc_PersonComponent },
  { path: 'menu', component: Kmmc_MenuComponent },
  { path: 'users', component: Kmmc_UserComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
