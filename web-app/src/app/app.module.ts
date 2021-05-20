import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { UserComponent } from './user/user.component';
import { PhotosComponent } from './photos/photos.component';
import { AlbumsComponent } from './albums/albums.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { AppRoutingModule } from './app-routing.module';
import {UsersService} from './services/users.service';
import {PhotosService} from './services/photos.service';
import {AlbumsService} from './services/albums.service';
import { UserDetailsComponent } from './user-details/user-details.component';

//import { UserDetailsComponent } from './user-details/user-details.component';
//import { UserDetailsComponent } from './user-details/user-details.component';
//import { UserDetailComponent } from './user/user-detail/user-detail.component';
//import { UserListComponent } from './user/user-list/user-list.component';



@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    PhotosComponent,
    AlbumsComponent,
    PageNotFoundComponent,
    UserDetailsComponent,
  //  UserDetailsComponent,
  //  UserDetailsComponent,
    //UserDetailComponent,
    //UserListComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule
    
  ],
  providers: [UsersService,PhotosService,AlbumsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
