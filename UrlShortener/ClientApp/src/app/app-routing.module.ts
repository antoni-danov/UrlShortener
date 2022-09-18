import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditUrlComponent } from './components/edit-url/edit-url.component';
import { HomeComponent } from './components/home/home.component';
import { ShortUrlComponent } from './components/short-url/short-url.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { UrlDetailsComponent } from './components/url-details/url-details.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { AuthGuard } from './shared/guard/auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'shorturl', component: ShortUrlComponent },
  { path: 'login', component: SignInComponent},
  { path: 'register', component: SignUpComponent },
  { path: 'profile', component: UserProfileComponent, canActivate: [AuthGuard] },
  { path: 'editUrl', component: EditUrlComponent, canActivate:[AuthGuard]},
  { path: 'urlDetails', component: UrlDetailsComponent, canActivate:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
