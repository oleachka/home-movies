import { NgModule } from '@angular/core';
import { CommonModule, LocationStrategy, HashLocationStrategy } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { AlertModule } from 'ngx-bootstrap';


import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';

import { StarComponent } from './components/star/star.component';
import { NewMovieComponent } from './components/newmovie/newmovie.component';
import { AppComponent } from './components/app/app.component';
import { MovieDataService } from './services/movie-data.service';

import { ModalModule } from 'ngx-bootstrap';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown'
import { IMDBComponent } from './components/imdb/imdb.component';

@NgModule({
  declarations: [
      AppComponent,
      NavMenuComponent,
      HomeComponent,
      StarComponent,
      NewMovieComponent,
      IMDBComponent
      
  ],
  imports: [
      AlertModule.forRoot(),
      ModalModule.forRoot(),
      BsDropdownModule.forRoot(),
      CommonModule,
      HttpModule,
      FormsModule,
      BrowserModule,      
      RouterModule.forRoot([
          { path: '', redirectTo: 'home', pathMatch: 'full' },
          { path: 'home', component: HomeComponent },
          { path: 'add-movie', component: NewMovieComponent },
          { path: 'imdb-info/:movieTitle', component: IMDBComponent },
          { path: '**', redirectTo: 'home' }
      ]),
      
  ],
  providers: [       
      MovieDataService,      
      { provide: 'BASE_URL', useFactory: getBaseUrl }     
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
