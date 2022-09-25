import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { ShortUrlComponent } from './components/short-url/short-url.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { UrlDetailsComponent } from './components/url-details/url-details.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { AuthGuard } from './shared/guard/auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'shorturl', component: ShortUrlComponent },
  { path: 'login', component: SignInComponent },
  { path: 'register', component: SignUpComponent },
  {
    path: '', canActivateChild: [AuthGuard], children: [
      { path: 'profile', component: UserProfileComponent },
      { path: 'urldetails/:id', component: UrlDetailsComponent }
    ]
  },
  {
    path: '**', pathMatch: 'full',
    component: PageNotFoundComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
