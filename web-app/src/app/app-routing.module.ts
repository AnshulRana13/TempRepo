import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './user/user.component';
import { PhotosComponent } from './photos/photos.component';
import { AlbumsComponent } from './albums/albums.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { UserDetailsComponent } from './user-details/user-details.component';
//import { UserListComponent } from './user/user-list/user-list.component';
//import { UserDetailComponent } from './user/user-detail/user-detail.component';


const appRoutes: Routes = [
  { path: 'users', component: UserComponent  },
  { path: 'photos', component: PhotosComponent },
  { path: 'albums', component: AlbumsComponent },
  { path: 'detail/:id', component : UserDetailsComponent},
  { path: '',   redirectTo: '/users', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true , useHash: true } // <-- debugging purposes only
    )
  ],
  exports: [
    RouterModule
  ]
  
})
export class AppRoutingModule { }
