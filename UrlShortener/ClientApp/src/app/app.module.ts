import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { ShareButtonsModule } from 'ngx-sharebuttons/buttons';
import { ShareIconsModule } from 'ngx-sharebuttons/icons';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { HomeComponent } from './components/home/home.component';
import { ShortUrlComponent } from './components/short-url/short-url.component';
import { ClipboardModule } from 'ngx-clipboard';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';

import { AuthServicesService } from './services/auth/auth-services.service';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { UserService } from './services/User/user.service';
import { ShortServiceService } from './services/ShorteningURL/short-service.service';
import { UrlDetailsComponent } from './components/url-details/url-details.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
<<<<<<< HEAD
import { environment } from '../environments/environment.prod';
=======
>>>>>>> f791317d5baabd14d93217b495ab8e033e4ed2e1

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FooterComponent,
    HomeComponent,
    ShortUrlComponent,
    SignInComponent,
    SignUpComponent,
    UserProfileComponent,
    UrlDetailsComponent,
    PageNotFoundComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    ClipboardModule,
    ShareButtonsModule,
<<<<<<< HEAD
    ShareIconsModule,
=======
    ShareIconsModule
>>>>>>> f791317d5baabd14d93217b495ab8e033e4ed2e1
  ],
  providers: [
    AuthServicesService,
    ShortServiceService,
    UserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
